using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BeatSaberModManager.DataModels;
using static System.Windows.Forms.ListView;

namespace BeatSaberModManager.Core
{
    class GroupComparer : IComparer<ListViewGroup>
    {
        public int Compare(ListViewGroup groupA, ListViewGroup groupB)
        {
            if (groupA.Header == "Other")
                return 1;
            if (groupB.Header == "Other")
                return -1;

            float weightA = CalculateWeight(groupA.Items);
            float weightB = CalculateWeight(groupB.Items);

            float compareWeights = weightB - weightA;
            if (compareWeights != 0f)
                return (int)compareWeights;

            return groupA.Header.CompareTo(groupB.Header);
        }

        float CalculateWeight(ListViewItemCollection items)
        {
            float weight = 0;

            foreach (ListViewItem item in items)
            {
                ReleaseInfo release = (ReleaseInfo)item.Tag;
                weight += release.weight;
            }

            return weight / items.Count;
        }
    }
}
