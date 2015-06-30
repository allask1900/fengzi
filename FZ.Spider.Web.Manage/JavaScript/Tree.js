function OnTreeNodeChecked() 
{ 
	var ele = event.srcElement; 
	if(ele.type=='checkbox') 
	{ 
		var childrenDivID = ele.id.replace('CheckBox','Nodes'); 
		var div = document.getElementById(childrenDivID); 
		if(div != null) 
		{ 
			var checkBoxs = div.getElementsByTagName('INPUT'); 
			for(var i=0;i <checkBoxs.length;i++) 
			{ 
				if(checkBoxs[i].type=='checkbox') 
				checkBoxs[i].checked=ele.checked; 
			} 
			var div = GetParentByTagName(ele,'DIV'); 
			var checkBoxs = div.getElementsByTagName('INPUT'); 
			var parentCheckBoxID = div.id.replace('Nodes','CheckBox'); 
			var parentCheckBox = document.getElementById(parentCheckBoxID); 
			for(var i=0;i <checkBoxs.length;i++) 
		  { 
				if(checkBoxs[i].type=='checkbox' && checkBoxs[i].checked) 
			  { 
					parentCheckBox.checked = true; 
					return; 
				} 
			} 
			parentCheckBox.checked = false; 
		} 
		else 
		{ 
			var div = GetParentByTagName(ele,'DIV'); 
			var checkBoxs = div.getElementsByTagName('INPUT'); 
			var parentCheckBoxID = div.id.replace('Nodes','CheckBox'); 
			var parentCheckBox = document.getElementById(parentCheckBoxID); 
			
			var basediv = GetParentByTagName(parentCheckBox,'DIV'); 
			var parentCBKs = basediv.getElementsByTagName('INPUT'); 
			var baseCheckBoxID = basediv.id.replace('Nodes','CheckBox'); 
			var baseCheckBox = document.getElementById(baseCheckBoxID); 
			
			for(var i=0;i <checkBoxs.length;i++) 
			{ 
				if(checkBoxs[i].type=='checkbox' && checkBoxs[i].checked) 
			  { 
					parentCheckBox.checked = true; 
					for(var j=0;j <parentCBKs.length;j++) 
					{ 
						if(parentCBKs[j].type=='checkbox' &&  parentCBKs[j].checked) 
						{ 
							baseCheckBox.checked = true; 
						} 
					} 
					return; 
				} 
			} 
			parentCheckBox.checked = false; 
			var checkedcount = 0; 
			for(var j=0;j <parentCBKs.length;j++) 
			{ 
				if(parentCBKs[j].type=='checkbox' &&  parentCBKs[j].checked) 
				{ 
					checkedcount++; 
				} 
			} 
			if(checkedcount == 0) 
			{ 
				baseCheckBox.checked = false; 
			} 
		} 
		
	} 
} 

function GetParentByTagName(element, tagName) 
{ 
	var parent = element.parentNode; 
	var upperTagName = tagName.toUpperCase(); 
	while (parent && (parent.tagName.toUpperCase() != upperTagName)) 
	{ 
		parent = parent.parentNode ? parent.parentNode : parent.parentElement; 
	} 
	return parent; 
} 
