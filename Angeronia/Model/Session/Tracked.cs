using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Angeronia.Model.Session
{
    public class Tracked
    {
        public string Name { get; private set; }

        public string ImageUrl { get; private set; }

        public bool IsSignatureValid { get; private set; }

        public string Fingerprint { get; private set; }

        public Tracked(string name, string imageUrl, string fingerprint, bool validSignature)
        {
            Name = name;
            ImageUrl = imageUrl;
            IsSignatureValid = validSignature;
            Fingerprint = fingerprint.ToUpper();
        }
    }
}
