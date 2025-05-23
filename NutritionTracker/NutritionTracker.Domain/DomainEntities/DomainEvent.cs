﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionTracker.Domain.DomainEntities
{
    public class DomainEvent
	{ 
        public Guid Id { get; set; }
		public int Version { get; set; }
		public string EventType { get; set; }
		public string Data { get; set; } 
		public DateTime Timestamp { get; set; }
		public Guid UserId { get; set; }
	}
}
