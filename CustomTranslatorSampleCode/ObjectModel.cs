using System;
using System.Collections.Generic;

namespace CustomTranslatorSampleCode
{
    public class ProjectCreateRequest
    {
        public string name { get; set; }
        public int languagePairId { get; set; }
        public int categoryid { get; set; }
        public string categoryDescriptor { get; set; }
        public string label { get; set; }
        public string description { get; set; }
    }

    public class ProjectEditRequest
    {
        public string name { get; set; }
        public string categoryDescriptor { get; set; }
        public string description { get; set; }
    }

    public class DocumentDetailsForImportRequest
    {
        public string DocumentName { get; set; }
        public string DocumentType { get; set; } //values = training, tuning, testing
        public bool IsParallel { get; set; }
        public List<FileForImportRequest> FileDetails { get; set; }
    }

    public class FileForImportRequest
    {
        public string Name { get; set; }
        public string Language { get; set; }
        public string Type { get; set; }
        public bool OverwriteIfExists { get; set; }
    }

    public class ModelCreateRequest
    {
        public string name { get; set; }
        public string projectId { get; set; }
        public List<int> documentIds { get; set; }
        public bool isTuningAuto { get; set; }
        public bool isTestingAuto { get; set; }
        public bool isAutoDeploy { get; set; }
        public float autoDeployThreshold { get; set; }
    }

    public class ModelEditRequest
    {
        public string name { get; set; }
    }

    public class FileUploadJob
    {
        public string jobId { get; set; }
        public List<string> filesAcceptedForProcessing { get; set; }
    }

    public class FileUploadStatus
    {
        public string displayName { get; set; }
        public int id { get; set; }
    }

    public class FileProcessingStatus
    {
        public FileUploadStatus status { get; set; }
        public DateTime modifiedDate { get; set; }
        public string fileName { get; set; }
        public object documentName { get; set; }
        public object summary { get; set; }
        public int id { get; set; }
        public object parentId { get; set; }
        public object language { get; set; }
    }

    public class CurrentFileUploadStatus
    {
        public object jobName { get; set; }
        public List<FileProcessingStatus> fileProcessingStatus { get; set; }
        public int pageIndex { get; set; }
        public int totalPageCount { get; set; }
    }

    public class Category
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Language
    {
        public int id { get; set; }
        public string displayName { get; set; }
        public string languageCode { get; set; }
    }


    public class LanguagePair
    {
        public int id { get; set; }
        public Language sourceLanguage { get; set; }
        public Language targetLanguage { get; set; }
    }

    public class CreatedBy
    {
        public string id { get; set; }
        public string userName { get; set; }
    }

    public class Project
    {
        public string id { get; set; }
        public string name { get; set; }
        public string label { get; set; }
        public string description { get; set; }
        public LanguagePair languagePair { get; set; }
        public Category category { get; set; }
        public string categoryDescriptor { get; set; }
        public object baselineBleuScorePunctuated { get; set; }
        public object bleuScorePunctuated { get; set; }
        public object baselineBleuScoreUnpunctuated { get; set; }
        public object bleuScoreUnpunctuated { get; set; }
        public object baselineBleuScoreCIPunctuated { get; set; }
        public object bleuScoreCIPunctuated { get; set; }
        public object baselineBleuScoreCIUnpunctuated { get; set; }
        public object bleuScoreCIUnpunctuated { get; set; }
        public string status { get; set; }
        public DateTime modifiedDate { get; set; }
        public DateTime createdDate { get; set; }
        public CreatedBy createdBy { get; set; }
        public object modifiedBy { get; set; }
        public string apiDomain { get; set; }
        public bool isAvailable { get; set; }
    }

    public class FileInDocument
    {
        public int id { get; set; }
        public string fileName { get; set; }
        public Language language { get; set; }
        public DateTime uploadDate { get; set; }
        public int extractedSentenceCount { get; set; }
    }


    public class DocumentInfo
    {
        public int id { get; set; }
        public string name { get; set; }
        public string documentType { get; set; }
        public bool isParallel { get; set; }
        public object alignedSentenceCount { get; set; }
        public CreatedBy createdBy { get; set; }
        public object modifiedBy { get; set; }
        public List<FileInDocument> files { get; set; }
        public List<Language> languages { get; set; }
        public DateTime createdDate { get; set; }
        public bool isAvailable { get; set; }
    }

    public class ProcessedDocumentInfo
    {
        public int id { get; set; }
        public int modelId { get; set; }
        public int documentId { get; set; }
        public int alignedSentenceCount { get; set; }
        public int usedSentenceCount { get; set; }
    }

    public class Document
    {
        public DocumentInfo documentInfo { get; set; }
        public ProcessedDocumentInfo processedDocumentInfo { get; set; }
    }

    public class TrainingModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public object modelIdentifier { get; set; }
        public string projectId { get; set; }
        public List<Document> documents { get; set; }
        public double? baselineBleuScorePunctuated { get; set; }
        public double? bleuScorePunctuated { get; set; }
        public double? baselineBleuScoreUnpunctuated { get; set; }
        public double? bleuScoreUnpunctuated { get; set; }
        public double? baselineBleuScoreCIPunctuated { get; set; }
        public double? bleuScoreCIPunctuated { get; set; }
        public double? baselineBleuScoreCIUnpunctuated { get; set; }
        public double? bleuScoreCIUnpunctuated { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? completionDate { get; set; }
        public DateTime modifiedDate { get; set; }
        public DateTime createdDate { get; set; }
        public CreatedBy createdBy { get; set; }
        public object modifiedBy { get; set; }
        public int trainingSentenceCount { get; set; }
        public int tuningSentenceCount { get; set; }
        public int testingSentenceCount { get; set; }
        public int dictionarySentenceCount { get; set; }
        public int? monolingualSentenceCount { get; set; }
        public string modelStatus { get; set; }
        public string statusInfo { get; set; }
        public bool? isTuningAuto { get; set; }
        public bool? isTestingAuto { get; set; }
        public bool isAutoDeploy { get; set; }
        public object autoDeployThreshold { get; set; }
    }
}
