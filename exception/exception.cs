using System;

namespace BibliothequeNumerique.Exceptions
{
    public class DocumentNonTrouveException : Exception
    {
        public DocumentNonTrouveException() : base("Document non trouvé.")
        {
        }

        public DocumentNonTrouveException(string message) : base(message)
        {
        }

        public DocumentNonTrouveException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }
}