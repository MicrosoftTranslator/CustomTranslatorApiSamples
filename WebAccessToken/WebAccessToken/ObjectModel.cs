using System;
using System.Collections.Generic;

namespace TextTranslatorSampleCode
{
    class TextTranslatorProjectCreateRequest
    {
        public string name { get; set; }
        public int languagePairId { get; set; }
        public int categoryid { get; set; }
        public string categoryDescriptor { get; set; }
        public string label { get; set; }
        public string description { get; set; }
    }

    class TextTranslatorProjectEditRequest
    {
        public string name { get; set; }
        public string categoryDescriptor { get; set; }
        public string description { get; set; }
    }

    class TextTranslatorDocumentDetailsForImportRequest
    {
        public string DocumentName { get; set; }
        public string DocumentType { get; set; } //values = training, tuning, testing
        public bool IsParallel { get; set; }
        public List<TextTranslatorFileForImportRequest> FileDetails { get; set; }
    }

    class TextTranslatorFileForImportRequest
    {
        public string Name { get; set; }
        public string Language { get; set; }
        public string Type { get; set; }
        public bool OverwriteIfExists { get; set; }
    }

    class TextTranslatorModelCreateRequest
    {
        public string name { get; set; }
        public string projectId { get; set; }
        public List<int> documentIds { get; set; }
        public bool isTuningAuto { get; set; }
        public bool isTestingAuto { get; set; }
        public bool isAutoDeploy { get; set; }
        public float autoDeployThreshold { get; set; }
    }

    class TextTranslatorModelEditRequest
    {
        public string name { get; set; }
    }

    
}
