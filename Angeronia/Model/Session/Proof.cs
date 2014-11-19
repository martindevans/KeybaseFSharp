
namespace Angeronia.Model.Session
{
    public class Proof
    {
        private readonly string _type;
        public string Type { get { return _type; } }

        private readonly string _nametag;
        public string Nametag { get { return _nametag; } }

        private readonly string _proofUrl;
        public string ProofUrl { get { return _proofUrl; } }

        private readonly bool _isProofValid;
        public bool IsProofValid { get { return _isProofValid; } }

        public Proof(string type, string nametag, string proofUrl, bool isProofValid)
        {
            _type = type;
            _nametag = nametag;
            _proofUrl = proofUrl;
            _isProofValid = isProofValid;
        }
    }
}
