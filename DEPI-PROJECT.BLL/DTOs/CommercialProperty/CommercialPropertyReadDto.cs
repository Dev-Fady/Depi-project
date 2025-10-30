﻿using DEPI_PROJECT.BLL.DTOs.PropertyGallery;
using DEPI_PROJECT.DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.BLL.DTOs.CommercialProperty
{
    public class CommercialPropertyReadDto
    {
        public Guid PropertyId { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string GoogleMapsUrl { get; set; }

        public PropertyType PropertyType { get; set; }
        public PropertyPurpose PropertyPurpose { get; set; }
        public PropertyStatus PropertyStatus { get; set; }

        public decimal Price { get; set; }
        public float Square { get; set; }
        public string Description { get; set; }

        public string AgentName { get; set; }
        public string? CompoundName { get; set; }

        public string BusinessType { get; set; }
        public int FloorNumber { get; set; }
        public bool HasStorage { get; set; }
        public CommercialAmenityReadDto Amenity { get; set; }
        public List<PropertyGalleryReadDto> Galleries { get; set; }
    }
}
