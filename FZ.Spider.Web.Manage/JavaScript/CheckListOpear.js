
//全选 --> flag=1 反选 flag=0   
function selectAll(root, leaf)
{
    var inputs = document.forms[0].elements;
    for (var i=0; i < inputs.length; i++)
	{
		if (inputs[i].type == 'checkbox' && inputs[i].id.indexOf(root) > -1)
		{
			if (document.getElementById(leaf).checked)
				inputs[i].checked = true;
		   else
				inputs[i].checked = false;
		}
	}
} 


//全选 --> flag=1 反选 flag=0   
function selectAll2(root, leaf, moduleid, itemid)
{
    var leafCheck = false;
    //alert(leaf);
    var inputs = document.forms[0].elements;
    for (var i=0; i < inputs.length; i++)
	{

		if (inputs[i].type == 'checkbox' && inputs[i].id.indexOf(leaf) > -1)
		{
			if (inputs[i].checked)
				leafCheck = true;
		}
	}
	
	//alert(leafCheck);
	if (leafCheck)
	    inputs[root].checked = true;
    else
		inputs[root].checked = false;
		
	if (moduleid != 0)
    {
	    if (!inputs[moduleid].checked)
	    {
	        for (var i=0; i < inputs.length; i++)
	        {
		        if (inputs[i].type == 'checkbox' && inputs[i].id.indexOf(itemid) > -1)
		        {
			        inputs[i].checked = false;
		        }
	        }
	    }
	}
} 


//全选 --> flag=1 反选 flag=0   
function selectAll3(root, root0, root2,root3, leaf)
{
    var leafCheck = false;
    //alert(root2);
    //alert(root3);
    //alert(leaf);
    var inputs = document.forms[0].elements;
    for (var i=0; i < inputs.length; i++)
	{

		if (inputs[i].type == 'checkbox' && inputs[i].id.indexOf(root2) > -1 && inputs[i].id != root3)
		{
			if (inputs[i].checked)
				leafCheck = true;
		}
	}
	
	//alert(leafCheck);
	//alert(leafCheck);
	if (leafCheck)
	{
	    //inputs[root].checked = true;
	    inputs[root3].checked = true;
	}
    else
    {
		//inputs[root].checked = false;
		inputs[root3].checked = false;
	}
	
	selectAll2(root, root0, 0, 0);
} 
