using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heirloom.OpenGLES
{
    public enum FramebufferStatus
    {
        Complete = 0x8CD5,
        IncompleteAttachment = 0x8CD6,
        IncompleteDimensions = 0x8CD9,
        IncompleteMissingAttachment = 0x8CD7,
        Unsupported = 0x8CDD
    }
}
