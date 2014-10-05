﻿using RestSharp;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Keybase;

namespace Angeronia.Model.Session
{
    public class InMemorySession
        : ISession
    {
        public Keybase.User.User User { get; private set; }

        public RestClient Client { get; private set; }

        private ObservableCollection<Tracked> _tracking = new ObservableCollection<Tracked>();
        public ObservableCollection<Tracked> Tracking
        {
            get { return _tracking; }
        }

        private ObservableCollection<Proof> _proofs = new ObservableCollection<Proof>();
        public ObservableCollection<Proof> Proofs
        {
            get { return _proofs; }
        }

        public InMemorySession(RestClient session, Keybase.User.User user)
        {
            Client = session;
            User = user;

            Task.Factory.StartNew(GetTrackees);
            Task.Factory.StartNew(GetProofs);
        }

        private void GetTrackees()
        {
            var sigs = Sig.User(User.Id);

            //Get all signatures, which are tracking signatures
            //Then group by user id and get the latest tracking information for a user
            var tracks = sigs.Sigs
                .Where(a => a.SignatureType == Sig.SignatureType.Track)
                .Select(sig => new
                {
                    sig,
                    json = sig.AsTrackeeSignature
                })
                .GroupBy(a => a.json.Body.Track.Id)
                .Select(a => a.Aggregate((x, y) => x.json.CTime > y.json.CTime ? x : y));

            foreach (var trackee in tracks)
            {
                Thread.Sleep(50);

                bool validateSignature = ValidateSignature(trackee.sig.PayloadJson, trackee.sig.Signature);

                var publicKeyFingerprint = trackee.json.Body.Track.Key.KeyFingerprint;
                var user = Keybase.User.Lookup.KeyFingerprint(publicKeyFingerprint);

                if (user.Them.Length > 0)
                {   
                    var username = trackee.json.Body.Track.Basics.Username;
                    var image = (user.Them[0].Pictures == null || user.Them[0].Pictures.Primary == null) ? "/Images/no_photo.png" : user.Them[0].Pictures.Primary.Url;

                    var tracked = new Tracked(username, image, publicKeyFingerprint, validateSignature);
                    App.Current.Dispatcher.Invoke(() => _tracking.Add(tracked));
                }
            }
        }

        private void GetProofs()
        {
            Parallel.ForEach(User.ProofsSummary.All, proof =>
            {
                //todo: validate proof
                var signature = Sig.SigId(proof.SignatureId);
                bool validateSignature = ValidateSignature(null, null);

                var prf = new Proof(proof.ProofType);
                App.Current.Dispatcher.Invoke(() => _proofs.Add(prf));
            });
        }

        private int _isValid = 0;
        private bool ValidateSignature(string payload, string signature)
        {
            return Interlocked.Increment(ref _isValid) % 2 == 1;
            return true;    //todo: Check signature
        }
    }
}
