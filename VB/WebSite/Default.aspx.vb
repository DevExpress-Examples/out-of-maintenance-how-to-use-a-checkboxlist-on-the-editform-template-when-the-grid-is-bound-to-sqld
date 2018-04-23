Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Collections.Generic
Imports DevExpress.Web.Data
Imports System.Collections
Imports DevExpress.Web

Partial Public Class _Default
	Inherits System.Web.UI.Page
	Public Const CategorySeparator As String = ", "

	Private Function GetCategoriesFromList() As String
		Dim categories As String = String.Empty
		Dim list As CheckBoxList = CType(Grid.FindEditRowCellTemplateControl(CType(Grid.Columns(2), GridViewDataColumn), "List"), CheckBoxList)

		' workaround
		If Grid.IsCallback Then
			LoadListControlPostDataOnCallback(list)
		End If

		For Each item As ListItem In list.Items
			If item.Selected Then
				categories &= item.Value & CategorySeparator
			End If
		Next item
		If categories.EndsWith(CategorySeparator) Then
			categories = categories.Substring(0, categories.Length - CategorySeparator.Length)
		End If
		Return categories
	End Function

	Protected Sub Grid_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs)
		e.NewValues("Categories") = GetCategoriesFromList()
	End Sub
	Protected Sub Grid_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs)
		e.NewValues("Categories") = GetCategoriesFromList()
	End Sub
	Protected Sub List_DataBound(ByVal sender As Object, ByVal e As EventArgs)
		Dim list As CheckBoxList = CType(sender, CheckBoxList)
		Dim container As GridViewEditItemTemplateContainer = CType(list.Parent, GridViewEditItemTemplateContainer)
		If container.Grid.IsNewRowEditing Then
			Return
		End If

		Dim values As Object = container.Grid.GetRowValues(container.VisibleIndex, "Categories")
		If values Is Nothing OrElse values Is DBNull.Value Then
			Return
		End If

		Dim categories As String = CStr(values)
		If categories Is Nothing Then
			Return
		End If

		Dim categoriesArray() As String = categories.Split(New String() { CategorySeparator }, StringSplitOptions.None)
		For Each item As ListItem In list.Items
			item.Selected = False
			For Each category As String In categoriesArray
				If category.Equals(item.Value) Then
					item.Selected = True
				End If
			Next category
		Next item
	End Sub

	' workaround for std ListControl LoadPostData
	Private Sub LoadListControlPostDataOnCallback(ByVal control As ListControl)
		If (Not Grid.IsEditing) Then
			Return
		End If
		For Each item As ListItem In control.Items
			item.Selected = False
		Next item
		For Each key As String In Request.Params.AllKeys
			Dim dataHandler As IPostBackDataHandler = TryCast(control, IPostBackDataHandler)
			If key.StartsWith(control.UniqueID) Then
				dataHandler.LoadPostData(key, Request.Params)
			End If
		Next key
	End Sub
End Class

