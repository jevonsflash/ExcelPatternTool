using System;
using GalaSoft.MvvmLight;

namespace Workshop.Model.Dto
{
    public class BaseDto: ObservableObject
    {
        public BaseDto()
        {
            Id = Guid.Empty;
        }
        public Guid Id { get; set; }
        public DateTime CreateTime { get; set; }
    }
}