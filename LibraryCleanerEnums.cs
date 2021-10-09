// Made with love by Ryan Boyer http://ryanjboyer.com <3

#if UNITY_EDITOR
using System;
using UnityEngine;

namespace LibraryCleaner
{
    public enum CleanCondition
    {
        Always,
        Threshold
    }

    public enum StorageUnit
    {
        [InspectorName("MB")] Megabyte,
        [InspectorName("GB")] Gigabyte
    }
}
#endif