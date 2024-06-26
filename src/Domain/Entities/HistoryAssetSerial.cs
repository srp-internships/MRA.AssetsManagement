﻿using MRA.AssetsManagement.Domain.Enums;

namespace MRA.AssetsManagement.Domain.Entities
{
    public record HistoryAssetSerial
    {
        public string Id { get; set; } = null!;
        public string Serial { get; set; } = null!;
        public Asset Asset { get; set; } = null!;
        public AssetStatus Status { get; set; }
        public UserDisplay? Employee { get; set; }
    }
}
