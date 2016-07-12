﻿namespace Sdl.Sdk.FileTypeSupport.Samples.Bil
{
    using Sdl.Core.Globalization;
    using Sdl.FileTypeSupport.Framework;
    using Sdl.FileTypeSupport.Framework.IntegrationApi;
    using Sdl.FileTypeSupport.Framework.NativeApi;

    /// <summary>
    /// Define Bil filter component builder.
    /// </summary> 
    [FileTypeComponentBuilderAttribute(Id = "Bil_FilterComponentBuilderExtension_Id",
                                       Name = "Bil_FilterComponentBuilderExtension_Name",
                                       Description = "Bil_FilterComponentBuilderExtension_Description")]
    public class BilFilterComponentBuilder : IFileTypeComponentBuilder
    {
        /// <summary>
        /// Gets or sets file type manager
        /// </summary>
        public IFileTypeManager FileTypeManager { get; set; }

        /// <summary>
        /// Gets or sets Filter Definition
        /// </summary>
        public IFileTypeDefinition FileTypeDefinition { get; set; }

        #region FileInfo
        /// <summary>
        /// Returns a file type information object.
        /// </summary>
        /// <param name="name">The <see cref="IFileTypeDefinition"/> will pass "" as the name for this parameter</param>
        /// <returns>an SimpleText file type information object</returns>
        public IFileTypeInformation BuildFileTypeInformation(string name)
        {
            var info = this.FileTypeManager.BuildFileTypeInformation();

            info.FileTypeDefinitionId = new FileTypeDefinitionId("BIL File Type 1.0.0.0");
            info.FileTypeName = new LocalizableString("Bilingual Sample File");
            info.FileTypeDocumentName = new LocalizableString("Bilingual XML Documen");
            info.FileTypeDocumentsName = new LocalizableString("Bilingual XML Documents");
            info.Description = new LocalizableString("Bilingual document format created for this SDK");
            info.FileDialogWildcardExpression = "*.bil";
            info.DefaultFileExtension = "bil";
            info.Icon = new IconDescriptor(PluginResources.bil);
            info.Enabled = true;

            return info;
        }
        #endregion
        /// <summary>
        /// Gets the file sniffer for this component.
        /// </summary>
        /// <param name="name">not used here</param>
        /// <returns>An Bil Native Filter Sniffer</returns>
        public INativeFileSniffer BuildFileSniffer(string name)
        {
            return new BilSniffer();
        }

        /// <summary>
        /// Gets the file extractor for this component.
        /// </summary>
        /// <param name="name">not used here</param>
        /// <returns>a FileExtractor containing an Bil Parser</returns>
        public IFileExtractor BuildFileExtractor(string name)
        {
            var parser = new BilParser();
            var extractor = this.FileTypeManager.BuildFileExtractor(parser, this);
            return extractor;
        }

        /// <summary>
        /// Gets the file generator for this component.
        /// </summary>
        /// <param name="name">not used herer</param>
        /// <returns><c>Null</c> if no file generator is defined</returns>
        public IFileGenerator BuildFileGenerator(string name)
        {
            return this.FileTypeManager.BuildFileGenerator(new BilWriter());
        }

        /// <summary>
        /// Gets the different sets of previews supported for this component.
        /// </summary>
        /// <param name="name">not used here</param>
        /// <returns>not implemented</returns>
        public IPreviewSetsFactory BuildPreviewSetsFactory(string name)
        {
            IPreviewSetsFactory previewFactory = FileTypeManager.BuildPreviewSetsFactory();

            return previewFactory;
        }

        /// <summary>
        /// Creates a new instance of the preview control with the specified name.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Should only be called from the main thread, as controls must always be
        /// instantiated on the same thread as the application message pump.
        /// </para>
        /// </remarks>
        /// <param name="name">not used here</param>
        /// <returns>not implemented</returns>
        public IAbstractPreviewControl BuildPreviewControl(string name)
        {
            return null;
        }

        /// <summary>
        /// Gets the QuickTags object for this component.
        /// </summary>
        /// <param name="name">not used here</param>
        /// <returns>a Quick tags factory</returns>
        public IQuickTagsFactory BuildQuickTagsFactory(string name)
        {
            return FileTypeManager.BuildQuickTagsFactory();
        }

        /// <summary>
        /// Gets the verifier list of this component.
        /// </summary>
        /// <param name="name">not used here</param>
        /// <returns>a verifier collection</returns>
        /// <remarks> The verifier list is an optional component for a file type.</remarks>
        public IVerifierCollection BuildVerifierCollection(string name)
        {
            return FileTypeManager.BuildVerifierCollection();
        }

        /// <summary>
        /// Gets a native or bilingual document generator of the type
        /// defined for the specified name.
        /// </summary>
        /// <param name="name">Abstract generator name</param>
        /// <returns>not generator for default preview</returns>
        public IAbstractGenerator BuildAbstractGenerator(string name)
        {
            return null;
        }

        /// <summary>
        /// The the additional generator information for this file type
        /// </summary>
        /// <param name="name">not used here</param>
        /// <returns>not implemented</returns>
        public IAdditionalGeneratorsInfo BuildAdditionalGeneratorsInfo(string name)
        {
            return null;
        }

        /// <summary>
        /// Gets the bilingual writer components for this component (if any).
        /// </summary>
        /// <param name="name">not used here</param>
        /// <returns><c>null</c> if no bilingual generator is defined</returns>
        public IBilingualDocumentGenerator BuildBilingualGenerator(string name)
        {
            return null;
        }

        /// <summary>
        /// Creates a new instance of the preview application with the specified name.
        /// Right now only allows to build expternal preview application.
        /// </summary>
        /// <param name="name">Preview application name</param>
        /// <returns>External preview application</returns>
        public IAbstractPreviewApplication BuildPreviewApplication(string name)
        {
            return null;
        }
    }
}