using System;
using System.Collections.Generic;
using System.Text;

namespace KPBrokers.Submission.Quote.API.Utilities
{
  public class StringEncryptorException : Exception
  {
    public StringEncryptorException(string message)
      : base(message)
    {
    }
  }
}
