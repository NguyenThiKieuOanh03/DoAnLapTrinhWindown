namespace QuanLyShopQuanAo.DATA
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThongTinSanPham")]
    public partial class ThongTinSanPham
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaSP { get; set; }

        [Required]
        [StringLength(50)]
        public string TenSP { get; set; }

        [Required]
        [StringLength(50)]
        public string Loai { get; set; }

        public int DonGia { get; set; }

        public int SoLuong { get; set; }

        [Required]
        [StringLength(50)]
        public string ChiTiet { get; set; }

        [Column(TypeName = "image")]
        public byte[] Anh { get; set; }
    }
}
