﻿using Dash.Forms.Helpers.Storage;
using Dash.Forms.Models.Storage;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dash.Forms.Models.Run
{
    public class RunData : DBBase
    {
        public string TrainingProgramId { get; set; }
        public int WeekNumber { get; set; }
        public int DayNumber { get; set; }
        public bool IsFreeRun { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public double Distance { get; set; }

        [Ignore()]
        public string DataDisplay
        {
            get
            {
                if (IsFreeRun == true)
                {
                    return $"{Start.ToShortDateString()} > {Elapsed.ToString(Elapsed.Hours > 0 ? "hh':'mm':'ss" : "mm':'ss")} > {Distance.ToString("N2")}";
                }
                return $"Week: {WeekNumber}, Day: {DayNumber} > {Elapsed.ToString(Elapsed.Hours > 0 ? "hh':'mm':'ss" : "mm':'ss")} > {Distance.ToString("N2")}";
            }
        }

        private IEnumerable<RunSegment> _Segments { get; set; }
        [Ignore()]
        public IEnumerable<RunSegment> Segments
        {
            get
            {
                if (_Segments == null)
                {
                    var storage = new RunSegmentStorageHelper();
                    var response = storage.GetByParentId(Id);
                    if (response != null)
                    {
                        _Segments = response.OrderBy(l => l.StartDate);
                    }
                }
                return _Segments;
            }
            set { _Segments = value; }
        }
        public TimeSpan Elapsed { get; set; }
    }
}
