using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Sidebucks.DAL.v1.Models {
    public class PinLocationModel {

        [JsonIgnore]
        public string Email { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public string Address { get; set; }

        public string CityName { get; set; }

        public string State { get; set; }

        public string CountryName { get; set; }

        public string PostalCode { get; set; }

        public string IpAddress { get; set; }

        public string MacAddress { get; set; }
    }

    public class PinLocationResponseModel {

        public PinLocationDetailResponseModel Location { get; set; }
    }

    public class PinLocationDetailResponseModel {

        public decimal? Latitude { get; set; }

        public decimal? Longitude { get; set; }

        public string Address { get; set; }

        public string CityName { get; set; }

        public string State { get; set; }

        public string CountryName { get; set; }

        public string PostalCode { get; set; }

        public string IpAddress { get; set; }

        public string MacAddress { get; set; }
    }
}
