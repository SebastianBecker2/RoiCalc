using System;
using System.Drawing;

namespace RoiCalc
{
    enum ItemType
    {
        Resource = 1,
        Plantation = 2,
        Livestock = 3,
        Tier1 = 4,
        Tier2 = 5,
        Tier3 = 6,
        CarPart = 7,
    }

    static class ItemTypeExtensions
    {
        public static Color ToBackColor(this ItemType type)
        {
            switch (type)
            {
                case ItemType.Resource:
                    return Color.Sienna;
                case ItemType.Plantation:
                    return Color.Chocolate;
                case ItemType.Livestock:
                    return Color.Coral;
                case ItemType.Tier1:
                    return Color.Gold;
                case ItemType.Tier2:
                    return Color.LimeGreen;
                case ItemType.Tier3:
                    return Color.CornflowerBlue;
                case ItemType.CarPart:
                    return Color.MediumOrchid;
                default:
                    throw new InvalidOperationException("The value of argument " + 
                        nameof(type) + " is invalid");
            }
        }

        public static string ToPrettyString(this ItemType type)
        {
            switch (type)
            {
                case ItemType.Resource:
                    return "Resource";
                case ItemType.Plantation:
                    return "Plantation";
                case ItemType.Livestock:
                    return "Livestock";
                case ItemType.Tier1:
                    return "Tier 1";
                case ItemType.Tier2:
                    return "Tier 2";
                case ItemType.Tier3:
                    return "Tier 3";
                case ItemType.CarPart:
                    return "Car Part";
                default:
                    throw new InvalidOperationException("The value of argument " +
                        nameof(type) + " is invalid");
            }
        }
    }
}
