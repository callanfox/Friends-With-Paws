﻿

#pragma checksum "C:\Users\Callan\Desktop\Project\FriendsWithPaws\FriendsWithPaws\SearchResultsPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "9230A622EC2F6E0CDD81EF47E5C557A3"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FriendsWithPaws
{
    partial class SearchResultsPage : global::FriendsWithPaws.Common.LayoutAwarePage, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 143 "..\..\SearchResultsPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.GoBack;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 107 "..\..\SearchResultsPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.Selector)(target)).SelectionChanged += this.Filter_SelectionChanged;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 123 "..\..\SearchResultsPage.xaml"
                ((global::Windows.UI.Xaml.Controls.ListViewBase)(target)).ItemClick += this.OnItemClick;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 82 "..\..\SearchResultsPage.xaml"
                ((global::Windows.UI.Xaml.Controls.ListViewBase)(target)).ItemClick += this.OnItemClick;
                 #line default
                 #line hidden
                break;
            case 5:
                #line 61 "..\..\SearchResultsPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ToggleButton)(target)).Checked += this.Filter_Checked;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


