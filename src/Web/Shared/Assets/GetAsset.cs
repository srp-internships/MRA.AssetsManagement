﻿using MRA.AssetsManagement.Web.Shared.AssetTypes;

namespace MRA.AssetsManagement.Web.Shared.Assets
{
    public class GetAsset
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public List<Properties> Properties { get; set; } = [];
        public string AssetTypeId { get; set; } = null!;

        public override string ToString()
        {
            var properties = string.Join(" ", Properties.Select(x => x.Value));
            return $"{Name} {properties}";
        }
    }
}
