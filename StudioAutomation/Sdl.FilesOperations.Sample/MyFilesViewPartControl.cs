﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Sdl.ProjectAutomation.Core;
using Sdl.TranslationStudioAutomation.IntegrationApi;

namespace Sdl.FilesOperations.Sample
{
    public partial class MyFilesViewPartControl : UserControl
    {
        public MyFilesViewPartControl()
        {
            InitializeComponent();
            GetFilesController().SelectedFilesChanged += OnSelectedFilesChanged;
        }    
        
        private FilesController GetFilesController()
        {
            return SdlTradosStudio.Application.GetController<FilesController>();
        }

        private void OnSelectedFilesChanged(object sender, EventArgs eventArgs)
        {
            RepopulateFilesList();
            OpenFileButton.Enabled = false;
        }

        private void RepopulateFilesList()
        {
            FilesListView.Items.Clear();
            FilesController filesController = GetFilesController();   
            foreach (ProjectFile file in filesController.SelectedFiles)
            {
                var item = new ListViewItem(file.Name)
                               {
                                   Tag = file
                               };
                item.SubItems.Add(file.AnalysisStatistics.Total.Words.ToString());
                
                FilesListView.Items.Add(item);
            }            
        }

        private void FilesListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            OpenFileButton.Enabled = FilesListView.SelectedItems.Count > 0;
        }

        private void FilesListView_ItemActivate(object sender, EventArgs e)
        {
            if (FilesListView.SelectedItems.Count == 0)
            {
                return;
            }

            SdlTradosStudio.Application.GetController<EditorController>().Open((ProjectFile)FilesListView.SelectedItems[0].Tag, EditingMode.Translation);
        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            var selectedProjectFiles = new List<ProjectFile>();
            for (int i = 0; i < FilesListView.SelectedItems.Count; i++)
            {
                selectedProjectFiles.Add((ProjectFile)FilesListView.SelectedItems[i].Tag);
            }

            if (selectedProjectFiles.Count == 0)
            {
                return;
            }

            try
            {
                SdlTradosStudio.Application.GetController<EditorController>()
                               .Open(selectedProjectFiles, EditingMode.Translation);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Cannot open the project file: \n{0}", ex.Message));
            }
        }

        private void AddFileButton_Click(object sender, EventArgs e)
        {
            GetFilesController().AddFiles();
        }        
    }
}
