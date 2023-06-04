# fit_20clc_hcmus_windows Paint Project  

Info: ID & Name & Email  

| ID          | Name|Email|
|-------------|---|---|
|20127598     |Lê Hoài Phương|lhphuong20@clc.fitus.edu.vn|
|20127605     |Nguyễn Minh Quang|nmquang20@clc.fitus.edu.vn|
|20127629     |Lăng Thảo Thảo|ltthao20@clc.fitus.edu.vn|
|20127679     |Lê Hoàng Khanh Nguyên|lhknguyen20@clc.fitus.edu.vn|  


## How to run our project?  

* 'Start with debugging' or 'Start without debugging' to run 'MainWindow'  


## What have been completed?  

* Dynamically load all graphic objects that can be drawn from external DLL files.  
* The user can choose which object to draw.  
* The user can see the preview of the object they want to draw.  
* The user can finish the drawing preview and their change becomes permanent with previously drawn objects.  
* The list of drawn objects can be saved and loaded again for continuing later.  
    
> You must save in your own defined binary format.  
> - Note: need to be updated when adding/updating/deleting any attributes of IShape object.  
    
* Save and load all drawn objects as an image in bmp/png/jpg format (rasterization). Just one format is fine. No need to save in all three formats.  

* Basic graphic objects:  
> 1. Line: controlled by two points, the starting point, and the endpoint  
> 2. Rectangle: controlled by two points, the left top point, and the right bottom point  
> 3. Ellipse: controlled by two points, the left top point, and the right bottom point   


## What have not been done?  

* None of the core requirements :D  


## What should be considered for bonus?  

* Allow the user to change the color, pen width, stroke type (dash, dot, dash dot dot...)  
* Adding image to the canvas  
* Adding text to the canvas
* Zooming in and out of canvas, return to original size  
* Undo, Redo  
* Save as image  
* Create new canvas and ask user if they want to save their work in progress's image  
* Clear screen

### In develop branch, we've been researching in custom Adorner for "Select a single element for editing again" feature


## Expected grade  

**10**

