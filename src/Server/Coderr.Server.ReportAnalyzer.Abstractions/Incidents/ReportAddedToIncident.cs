﻿using System;
using Coderr.Server.ReportAnalyzer.Abstractions.ErrorReports;

namespace Coderr.Server.ReportAnalyzer.Abstractions.Incidents
{
    /// <summary>
    ///     We just received a new report and attached it to the given incident.
    /// </summary>
    public class ReportAddedToIncident
    {
        /// <summary>
        ///     Creates a new instance of <see cref="ReportAddedToIncident" />.
        /// </summary>
        /// <param name="incident">incident that the report was added to</param>
        /// <param name="report">received report</param>
        /// <param name="isReOpened">Incident was marked as closed, so the received report opened it again.</param>
        /// <exception cref="ArgumentNullException">incident;report</exception>
        public ReportAddedToIncident(IncidentSummaryDTO incident, ReportDTO report, bool isReOpened)
        {
            if (incident == null) throw new ArgumentNullException("incident");
            if (report == null) throw new ArgumentNullException("report");

            Incident = incident;
            Report = report;
            IsReOpened = isReOpened;
        }

        /// <summary>
        ///     Serialization constructor
        /// </summary>
        protected ReportAddedToIncident()
        {
        }

        /// <summary>
        ///     Incident that the report was added to.
        /// </summary>
        public IncidentSummaryDTO Incident { get; private set; }

        /// <summary>
        ///     Incident was marked as closed, so the received report opened it again.
        /// </summary>
        public bool IsReOpened { get; set; }

        /// <summary>
        ///     Is the incident new, i.e. this is the first report for an incident.
        /// </summary>
        /// <remarks>
        ///     New parameter in later versions = nullable.
        /// </remarks>
        public bool? IsNewIncident { get; set; }

        /// <summary>
        ///     Received report.
        /// </summary>
        public ReportDTO Report { get; private set; }
    }
}