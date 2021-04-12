using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Workshop.Model.Apis
{

    public class InstructmentList
    {

        [JsonProperty("uniqueId")]
        public string UniqueId { get; set; }

        [JsonProperty("instrumentProducer")]
        public string InstrumentProducer { get; set; }

        [JsonProperty("instrumentType")]
        public string InstrumentType { get; set; }

        [JsonProperty("factoryId")]
        public string FactoryId { get; set; }

        [JsonProperty("instrumentId")]
        public string InstrumentId { get; set; }

        [JsonProperty("instrumentFunc")]
        public string[] InstrumentFunc { get; set; }

        [JsonProperty("instrumentName")]
        public string InstrumentName { get; set; }
    }

    public class SeatList
    {

        [JsonProperty("postName")]
        public string PostName { get; set; }

        [JsonProperty("seatId")]
        public string SeatId { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("noPark")]
        public bool NoPark { get; set; }

        [JsonProperty("instructmentList")]
        public InstructmentList[] InstructmentList { get; set; }
    }



    public class SeatInfoApi:InfoApiBase
    {


        [JsonProperty("seatList")]
        public SeatList[] SeatList { get; set; }
    }

}
