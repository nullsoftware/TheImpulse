using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public enum ItemStackBehavior : byte
{
    /// <summary>
    /// Items is not stackable.
    /// </summary>
    NotStackable,

    /// <summary>
    /// Item is stackable.
    /// </summary>
    Stackable,

    /// <summary>
    /// Item is not stackable and allows only one instance.
    /// </summary>
    Unique,
}
