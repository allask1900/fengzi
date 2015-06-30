if (!objbeforeItem) 
{ 
var objbeforeItem=null; 
var objbeforeItembackgroundColor=null; 
} 
function ItemOver(obj) 
{ 
if(objbeforeItem) 
{ 
	objbeforeItem.style.backgroundColor = objbeforeItembackgroundColor; 
} 
objbeforeItembackgroundColor = obj.style.backgroundColor; 
objbeforeItem = obj; 
obj.style.backgroundColor = "#e0eafe"; 
} 

function ItemOver2(obj) 
{ 
if(objbeforeItem) 
{ 
	objbeforeItem.style.backgroundColor = objbeforeItembackgroundColor; 
} 
objbeforeItembackgroundColor = obj.style.backgroundColor; 
objbeforeItem = obj; 
obj.style.backgroundColor = "#999999"; 
} 

