using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using DevExpress.Web.Data;
using System.Collections;
using DevExpress.Web.ASPxGridView;

public partial class _Default : System.Web.UI.Page {
    public const string CategorySeparator = ", ";

    private string GetCategoriesFromList() {
        string categories = string.Empty;
        CheckBoxList list = (CheckBoxList)Grid.FindEditRowCellTemplateControl((GridViewDataColumn)Grid.Columns[2], "List");

        // workaround
        if(Grid.IsCallback)
            LoadListControlPostDataOnCallback(list);

        foreach(ListItem item in list.Items) {
            if(item.Selected)
                categories += item.Value + CategorySeparator;
        }
        if(categories.EndsWith(CategorySeparator))
            categories = categories.Substring(0, categories.Length - CategorySeparator.Length);
        return categories;
    }

    protected void Grid_RowUpdating(object sender, ASPxDataUpdatingEventArgs e) {
        e.NewValues["Categories"] = GetCategoriesFromList();
    }
    protected void Grid_RowInserting(object sender, ASPxDataInsertingEventArgs e) {
        e.NewValues["Categories"] = GetCategoriesFromList();
    }
    protected void List_DataBound(object sender, EventArgs e) {
        CheckBoxList list = (CheckBoxList)sender;
        GridViewEditItemTemplateContainer container = (GridViewEditItemTemplateContainer)list.Parent;
        if(container.Grid.IsNewRowEditing) return;

        object values = container.Grid.GetRowValues(container.VisibleIndex, "Categories");
        if(values == null || values == DBNull.Value) return;

        string categories = (string)values;
        if(categories == null) return;

        string[] categoriesArray = categories.Split(new string[] { CategorySeparator }, StringSplitOptions.None);
        foreach(ListItem item in list.Items) {
            item.Selected = false;
            foreach(string category in categoriesArray)
                if(category.Equals(item.Value))
                    item.Selected = true;
        }
    }

    // workaround for std ListControl LoadPostData
    void LoadListControlPostDataOnCallback(ListControl control) {
        if(!Grid.IsEditing) return;
        foreach(ListItem item in control.Items)
            item.Selected = false;
        foreach(string key in Request.Params.AllKeys) {
            IPostBackDataHandler dataHandler = control as IPostBackDataHandler;
            if(key.StartsWith(control.UniqueID))
                dataHandler.LoadPostData(key, Request.Params);
        }
    }
}

