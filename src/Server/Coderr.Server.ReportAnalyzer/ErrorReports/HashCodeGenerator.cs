﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Coderr.Server.Domain.Core.ErrorReports;
using Griffin.Container;

namespace Coderr.Server.ReportAnalyzer.ErrorReports
{
    /// <summary>
    ///     Used to generate hash codes for incoming error reports
    /// </summary>
    /// <remarks>
    ///     This new generator uses context information provided in exceptions to generate the hash code. For instance HTTP 404
    ///     exception
    ///     are based on the URI and not exception information
    /// </remarks>
    [ContainerService]
    public class HashCodeGenerator : IHashCodeGenerator
    {
        private readonly IHashCodeSubGenerator[] _generators;
        private const string RemoveLineNumbersRegEx = @"^(.*)(:[\w]+ [\d]+)";

        /// <summary>
        ///     Creates a new instance of <see cref="HashCodeGenerator" />.
        /// </summary>
        /// <param name="generators">Specialized generators. treated as singletons.</param>
        public HashCodeGenerator(IEnumerable<IHashCodeSubGenerator> generators)
        {
            _generators = generators.ToArray();
        }

        /// <summary>
        ///     Generate a new hash code
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>hash code</returns>
        /// <exception cref="ArgumentNullException">entity</exception>
        public string GenerateHashCode(ErrorReportEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            foreach (var generator in _generators)
            {
                if (generator.CanGenerateFrom(entity))
                {
                    // forgiving ones so that we can get the report and process it with a default hash code instead.
                    var code = generator.GenerateHashCode(entity);
                    if (code != null)
                        return code;
                }
            }

            return DefaultCreateHashCode(entity);
        }

        /// <summary>
        ///     Method that will be invoked if no implementations of <see cref="IHashCodeSubGenerator" /> generates an hash code.
        /// </summary>
        /// <param name="report">received report</param>
        /// <returns>hash code</returns>
        /// <exception cref="ArgumentNullException">report</exception>
        protected virtual string DefaultCreateHashCode(ErrorReportEntity report)
        {
            if (report == null) throw new ArgumentNullException("report");

            //TODO: Ta bort radnummers stripparen
            var hashSource = report.Exception.FullName + "\r\n";
            hashSource += StripLineNumbers(report.Exception.StackTrace ?? "");

            var hash = 23;
            foreach (var c in hashSource)
            {
                hash = hash*31 + c;
            }
            return hash.ToString("X");
        }

        internal static string StripLineNumbers(string stacktrace)
        {
            var re = new Regex(RemoveLineNumbersRegEx, RegexOptions.Multiline);
            return re.Replace(stacktrace, "$1", 1000);
        }
    }
}