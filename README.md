# TaxonomyPickerControl

This custom K2 SmartForms control is based on the taxonomy Picker pattern on the Office 365 Patterns and Pracites site:

https://github.com/OfficeDev/PnP

The 'Core.TaxonomyPicker'- https://github.com/OfficeDev/PnP/tree/master/Components/Core.TaxonomyPicker

The goal of this project is to create a custom K2 SmartForms control with the same look and feel as the Taxonomy picker in SharePoint, using SmartObjects to retrieve and pick data instead of the native SharePoint JavaScript to retrieve values from SharePoint.

There are few advanatages that a custom control would bring over the use of the tree control in K2 (as this can be accomplished with the SmartForms native tree view control):

1. Familliar feel of SharePoint.
2. Less configuration - Only need to point at the correct SMO and have correct parameters, no need to conifgure a Tree control and rebuild the UI for this.
3. Performance - The SmartObject is only called once instead of once per a node expansion as it would be in the K2 native tree control.

Key architecutre points:
1. Most work is done in the taxonomypickercontrol.js file.  Here, any calls to the SharePoint.js were replaced to work with with a K2 SmartForm.
2. Data is coming from a 'Managed Metadata' smartobject, 'Get All Terms In Term Set' method.  This is done on control creation and data is then stored in JSON formatt in the Taxonomy Control 'data-taxJSON' attribute.  (This is currently hard-coded and should be improved).
3. Data is not yet saved in this control and this control is very much in Alpha phase.


Control setup and walkthrough.

1. Navigate to /_layouts/15/termstoremanager.aspx in a SharePoint site collection, and add terms to the term store.
![alt tag](https://github.com/markman623/TaxonomyPickerControl/blob/master/ReadMeImages/TermStoreAddTerms.png)
2. Add the custom control to the server, all resource files should be imbeded in the DLL so no need to transfer files
3. Add the control to a view, then go to it's settings
4. Get the Guide of the MMS Store, Term set, and Taxonomy SMO internal name you wish to use.  Enter these values in the settings.
![alt tag](https://github.com/markman623/TaxonomyPickerControl/blob/master/ReadMeImages/TaxonomyPickerSettings.png)
5. When you run the form you should then see the following:
![alt tag](https://github.com/markman623/TaxonomyPickerControl/blob/master/ReadMeImages/PickerRunning.png)
6. You can start typing and the suggestions box will populate:
![alt tag](https://github.com/markman623/TaxonomyPickerControl/blob/master/ReadMeImages/TaxSuggestions.png)
7. Or you can click the Tag to open the Picker tree:
![alt tag](https://github.com/markman623/TaxonomyPickerControl/blob/master/ReadMeImages/PickerTree.png)
8.This is how the picked terms will appear:
![alt tag](https://github.com/markman623/TaxonomyPickerControl/blob/master/ReadMeImages/PickedTerms.png)

9. If you update the Taxonomy store, you will have to do an IISReset to see that reflected in the control


You've probably noticed MANY many things that need to be improved in this control.  Grab a Fork and go for it!
