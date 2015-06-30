//表单用POP弹出窗口。
//完成于2008-4-30
//版本：1.0.1
//功能：指定位置（还可以智能判断外层窗口是否过小或自己的位置是否外移出BODY）、大小、内容引用，可以最大化，还原，记忆上一次位置和状态，可窗口托动和大小托动
//引用方式：showpopwin(testtext, 350, 200, 'http://www.baidu.com/')
//参数说明：依赖元素（让窗口依靠在哪个HTML元素位置显示），窗口宽，窗口高，内容引用的页面地址
//

//声明一些变量：鼠标点击处与窗口左边的距离,鼠标点击处与窗口上边的距离,事件处发目标,POP窗口最外层DIV,鼠标是否为按下的动作
var pop_divx, pop_divy, pop_element_obj, pop_windiv_obj = null, pop_fram_bk=null, pop_down_bool = false;
//用一个变量来记忆住现在窗口的状态
//初始状态，宽度，高度，左边距，上边距
var pop_max_mix = false, pop_init_width = 300, pop_init_height = 200, pop_init_left = 10, pop_init_top = 10;
//按下
function dragdiv()
{
    var x = parseInt(pop_windiv_obj.offsetLeft);
    var y = parseInt(pop_windiv_obj.offsetTop);
    pop_divx = event.clientX - x;
    pop_divy = event.clientY - y;
    
    pop_down_bool = true;
    pop_element_obj = event.srcElement;
    pop_element_obj.setCapture();

}
//移动
function moveHandler(zoom_move)
{
    if(pop_down_bool && !pop_max_mix)
    {
        if(zoom_move)
        {
            if((event.clientX - pop_init_left) > 100 && (event.clientY - pop_init_top) > 80)
            {
                pop_windiv_obj.style.width = (event.clientX - pop_init_left) + "px";
                pop_windiv_obj.style.height = (event.clientY - pop_init_top) + "px";
                pop_fram_bk.style.width = (event.clientX - pop_init_left) + "px";
                pop_windiv_obj.childNodes[1].style.height = (event.clientY - pop_init_top) - 22 - 15;
                
            }
        }
        else
        {
            
            if (event.clientX - pop_divx >0 && event.clientX - pop_divx+pop_init_width<document.body.clientWidth)
            {
                pop_windiv_obj.style.left = event.clientX - pop_divx;
                pop_fram_bk.style.left = pop_windiv_obj.style.left;
            }
            else if (event.clientX - pop_divx <0)
            {
                pop_windiv_obj.style.left=0;
                pop_fram_bk.style.left=0;
            }
            else if (event.clientX - pop_divx+pop_init_width>document.body.clientWidth)
            {
                pop_windiv_obj.style.left=document.body.clientWidth-pop_init_width;
                pop_fram_bk.style.left=pop_windiv_obj.style.left;
            }
            
            if (event.clientY - pop_divy>0 && event.clientY - pop_divy+pop_init_height<document.body.clientHeight)
            {
                pop_windiv_obj.style.top = event.clientY - pop_divy;
                pop_fram_bk.style.top = pop_windiv_obj.style.top;
            }
            else if (event.clientY - pop_divy<0)
            {
                pop_windiv_obj.style.top=0;
                pop_fram_bk.style.top=0;
            }
            else if (event.clientY - pop_divy+pop_init_height>document.body.clientHeight)
            {
                pop_windiv_obj.style.top=document.body.clientHeight-pop_init_height-5;
                pop_windiv_obj.style.top=pop_windiv_obj.style.top;
            }
            
        }
    }
}
//释放
function upHandler()
{
    pop_down_bool = false;
    pop_element_obj.releaseCapture();
    if(!pop_max_mix)
    {
        pop_init_left = pop_windiv_obj.offsetLeft;
        pop_init_top = pop_windiv_obj.offsetTop;
        if(pop_windiv_obj.offsetWidth != pop_init_width || pop_windiv_obj.offsetHeight != pop_init_height)
        {
            if(pop_windiv_obj.offsetWidth > 100 && pop_windiv_obj.offsetHeight > 80)
            {
                pop_init_width = pop_windiv_obj.offsetWidth;
                pop_init_height = pop_windiv_obj.offsetHeight;
            }
        }
        pop_fram_bk.style.left=pop_windiv_obj.style.left;
        pop_fram_bk.style.top = pop_windiv_obj.style.top;
    }   
}
//创建
function createpopwin(title)
{
    if(pop_windiv_obj != null)
    {
        closepopwin();
    }
    if(title == null)
    {
        title = "...";
    }
    //添加最外层DIV
    var newdivnode = document.createElement("div");
    newdivnode.className = "pop_window";
    newdivnode.style.zIndex = "100";
    document.body.appendChild(newdivnode);
    
    pop_windiv_obj = newdivnode;//打开窗口时就指定一POP窗的目标全局变量
    //添加标题行
    newdivnode = document.createElement("div");
    newdivnode.className = "pop_window_title";
    newdivnode.style.cursor = "move";//增加标题行鼠标样式。By Jamber
    newdivnode.onmousedown = function(){dragdiv();};
    newdivnode.onmousemove = function(){moveHandler(false);};
    newdivnode.onmouseup = function(){upHandler();};
    newdivnode.ondblclick = function(){maxpopwin();};
    pop_windiv_obj.appendChild(newdivnode);
    //添加标题行内的按钮
    newdivnode.innerHTML = "<div style='float:left;padding-left:5px;padding-top:3px;color:#FFF;'>" + title + "</div><div style='float:right;'><img id=\"popwin_resize_b\" alt=\"最大化\" src=\"../imgs/popwin/to_default.gif\" style=\"vertical-align:middle;width:15px;height:15px;cursor:default;\" onclick=\"maxpopwin()\" />&nbsp; <img alt=\"关\" src=\"../imgs/popwin/stop.gif\" style=\"vertical-align:middle; cursor:hand; width:15px; height:15px;\" onclick=\"closepopwin()\" /></div>";
    
    //添加内容框
    newdivnode = document.createElement("div");
    newdivnode.className = "pop_window_body";
    pop_windiv_obj.appendChild(newdivnode);
    //添加一个状态栏
    newdivnode = document.createElement("div");
    newdivnode.className = "pop_window_status";
    newdivnode.innerHTML = "<img src=\"../imgs/popwin/dragresize.gif\" style=\"vertical-align:middle;width:14px;height:13px;cursor:nw-resize;\" onmousedown=\"dragdiv();\" onmousemove=\"moveHandler(true);\" onmouseup=\"upHandler();\">";
    pop_windiv_obj.appendChild(newdivnode);
    
}

//显示窗口
//参数：POP窗口目标，依赖的页面对象元素，开启的窗口宽，高，调用的内容（资源列表的URL）
function showpopwin(dependobj, win_w, win_h, iframesrc, title)
{
    if(arguments.length < 4)
    {
        alert("JS调用的传入参数不正确");
        return;
    }
    //有的时候需要记忆窗体内容，所以用这种方式，如果不需要而去掉if，也就每次都是重新创建对象
    if(pop_windiv_obj == null)
    {
        createpopwin(title);
    }
    else
    {
        pop_windiv_obj.style.display = "block";
        pop_windiv_obj.firstChild.firstChild.innerHTML = title? title : "...";
        
    }
  
    //记忆窗口的的高宽
    if( document.body.clientWidth > win_w && win_w > 100)
    {
        pop_init_width = win_w;
    }
    if( document.body.clientHeight > win_h && win_w > 80)
    {
        pop_init_height = win_h;
    }
    
    //如果DOCUMENT窗口太小则主动放大
    if(document.body.clientWidth < ( pop_init_width + 10))
    {
        window.resizeTo((pop_init_width + 100), (document.body.clientHeight + 80));
    }
    if(document.body.clientHeight < ( pop_init_height + 10))
    {
        window.resizeTo((document.body.clientWidth + 10), (pop_init_height + 70));
    }
    //算出依赖对象的上下左右位置
    var t_dependobj_left = dependobj.offsetLeft;
    var t_dependobj_top = dependobj.offsetTop;
    
    var t_dependobj_right = dependobj.offsetWidth;
    var t_dependobj_bottom = dependobj.offsetHeight;
    t_dependobj_top += dependobj.offsetHeight;//把触发显示窗口元素给显示出来，别盖住了
    while (dependobj = dependobj.offsetParent)
    {
        t_dependobj_left += dependobj.offsetLeft;
        t_dependobj_top += dependobj.offsetTop;
    }
    //没有offsetRight，所以只能 窗口宽-左距-目标宽=右
    //同样offsetBottom也同样算出来
    t_dependobj_right = document.body.clientWidth - t_dependobj_left - t_dependobj_right;
    t_dependobj_bottom = document.body.clientHeight - t_dependobj_top - t_dependobj_bottom;
    //设定显示位置
    pop_init_left = t_dependobj_left;
    pop_init_top = t_dependobj_top;
    //算出会不会挤出网页BODY外，从而改变位置
    if(t_dependobj_right < pop_init_width)
    {
        pop_init_left = document.body.clientWidth - pop_init_width - 10;
    }
    if(t_dependobj_bottom < pop_init_height)
    {
        pop_init_top = document.body.clientHeight - pop_init_height - 10;
    }
    pop_windiv_obj.style.left = pop_init_left;
    pop_windiv_obj.style.top = pop_init_top + 2;
    pop_windiv_obj.style.width = pop_init_width;
    pop_windiv_obj.style.height = pop_init_height;
    if(pop_fram_bk == null)
    {
        pop_fram_bk = document.createElement('iframe');
        pop_fram_bk.id = 'fram_bk';
        document.body.appendChild(pop_fram_bk);
        pop_fram_bk.style.position = "absolute";
        pop_fram_bk.frameBorder = "0";
    }
    else
    {
        pop_fram_bk.style.display = "block";
    }
    pop_fram_bk.style.width = pop_init_width;
    pop_fram_bk.style.height = 28;
    pop_fram_bk.style.top = pop_init_top + 2;
    pop_fram_bk.style.left = pop_init_left;
    pop_fram_bk.style.zIndex = pop_windiv_obj.style.zIndex - 1;
 
    pop_windiv_obj.childNodes[1].style.height = pop_init_height - 22 - 15;//显示内容的DIV高度不能自动变化，所以强制一下。
    
    //给POP窗口添加内容
    if(iframesrc != null)
    {
        if(document.getElementById("pop_win_iframe") != null)
        {
            if(iframesrc != document.getElementById("pop_win_iframe").src)
            {
                pop_windiv_obj.childNodes[1].removeChild(document.getElementById("pop_win_iframe"));
                var iframenode = document.createElement("iframe");
                iframenode.id = "pop_win_iframe";
                iframenode.scrolling = "auto";//By Jamber
                iframenode.frameBorder = "0";
                iframenode.className = "iframeclass";
                iframenode.src = iframesrc;
                pop_windiv_obj.childNodes[1].appendChild(iframenode);
            }
        }
        else
        {
            var iframenode = document.createElement("iframe");
            iframenode.id = "pop_win_iframe";
            iframenode.scrolling = "auto";//By Jamber
            iframenode.frameBorder = "0";
            iframenode.className = "iframeclass";
            iframenode.src = iframesrc;
            pop_windiv_obj.childNodes[1].appendChild(iframenode);
        }
    }

}

//显示窗口
//参数：POP窗口目标，依赖的页面对象元素，开启的窗口宽，高，调用的内容（资源列表的URL）
function showwin( win_w, win_h, iframesrc, title)
{
    if(arguments.length < 4)
    {
        alert("JS调用的传入参数不正确");
        return;
    }
    //有的时候需要记忆窗体内容，所以用这种方式，如果不需要而去掉if，也就每次都是重新创建对象
    if(pop_windiv_obj == null)
    {
        createpopwin(title);
    }
    else
    {
        pop_windiv_obj.style.display = "block";
        pop_windiv_obj.firstChild.firstChild.innerHTML = title? title : "...";
        
    }
    //记忆窗口的的高宽
    if( document.body.clientWidth > win_w && win_w > 100)
    {
        pop_init_width = win_w;
    }
    if( document.body.clientHeight > win_h && win_w > 80)
    {
        pop_init_height = win_h;
    }
    
    //如果DOCUMENT窗口太小则主动放大
    if(document.body.clientWidth < ( pop_init_width + 10))
    {
        window.resizeTo((pop_init_width + 100), (document.body.clientHeight + 80));
    }
    if(document.body.clientHeight < ( pop_init_height + 10))
    {
        window.resizeTo((document.body.clientWidth + 10), (pop_init_height + 70));
    }
 
    //设定显示位置
    pop_init_left = (document.body.clientWidth-pop_init_width)/2-10;
    pop_init_top = (document.body.clientHeight-pop_init_height)/2-20;
    
    pop_windiv_obj.style.left = pop_init_left;
    pop_windiv_obj.style.top = pop_init_top + 2;
    pop_windiv_obj.style.width = pop_init_width;
    pop_windiv_obj.style.height = pop_init_height;
    pop_windiv_obj.childNodes[1].style.height = pop_init_height - 22 - 15;//显示内容的DIV高度不能自动变化，所以强制一下。
    
    if(pop_fram_bk == null)
    {
        pop_fram_bk = document.createElement('iframe');
        pop_fram_bk.id = 'fram_bk';
        document.body.appendChild(pop_fram_bk);
        pop_fram_bk.style.position = "absolute";
        pop_fram_bk.frameBorder = "0";
    }
    else
    {
        pop_fram_bk.style.display = "block";
    }
    pop_fram_bk.style.width = pop_init_width;
    pop_fram_bk.style.height = 30;
    pop_fram_bk.style.top = pop_init_top + 2;
    pop_fram_bk.style.left = pop_init_left;
    pop_fram_bk.style.zIndex = pop_windiv_obj.style.zIndex - 1;
    
    //给POP窗口添加内容
    if(iframesrc != null)
    {
        if(document.getElementById("pop_win_iframe") != null)
        {
            if(iframesrc != document.getElementById("pop_win_iframe").src)
            {
                pop_windiv_obj.childNodes[1].removeChild(document.getElementById("pop_win_iframe"));
                var iframenode = document.createElement("iframe");
                iframenode.id = "pop_win_iframe";
                iframenode.scrolling = "auto";//By Jamber
                iframenode.frameBorder = "0";
                iframenode.className = "iframeclass";
                iframenode.src = iframesrc;
                pop_windiv_obj.childNodes[1].appendChild(iframenode);
            }
        }
        else
        {
            var iframenode = document.createElement("iframe");
            iframenode.id = "pop_win_iframe";
            iframenode.scrolling = "auto";//By Jamber
            iframenode.frameBorder = "0";
            iframenode.className = "iframeclass";
            iframenode.src = iframesrc;
            pop_windiv_obj.childNodes[1].appendChild(iframenode);
        }
    }

}

//放大缩小
function maxpopwin()
{
    if(pop_windiv_obj != null)
    {
        if(!pop_max_mix)
        {
            pop_windiv_obj.style.left = 5;
            pop_windiv_obj.style.top = 5;
            pop_windiv_obj.style.width = document.body.clientWidth - 10;
            pop_windiv_obj.style.height = document.body.clientHeight - 10;
            pop_windiv_obj.childNodes[1].style.height = document.body.clientHeight - 10 - 22 - 15;
            pop_fram_bk.style.width = document.body.clientWidth - 10;
            pop_fram_bk.style.top = 5;
            pop_fram_bk.style.left = 5;
    
            pop_max_mix = true;
            document.getElementById("popwin_resize_b").alt = "向下还原";
            document.getElementById("popwin_resize_b").src = document.getElementById("popwin_resize_b").src.replace("_default", "_large");
        }
        else
        {
            pop_windiv_obj.style.left = pop_init_left;
            pop_windiv_obj.style.top = pop_init_top;
            pop_windiv_obj.style.width = pop_init_width;
            pop_windiv_obj.style.height = pop_init_height;
            pop_windiv_obj.childNodes[1].style.height = pop_init_height - 22 - 15;
            
            pop_fram_bk.style.width = pop_init_width;
            pop_fram_bk.style.top = pop_init_top;
            pop_fram_bk.style.left = pop_init_left;
            pop_max_mix = false;
            document.getElementById("popwin_resize_b").alt = "最大化";
            document.getElementById("popwin_resize_b").src = document.getElementById("popwin_resize_b").src.replace("_large", "_default");
        }
    }
    
}
//关闭窗口
function closepopwin()
{
    pop_windiv_obj.style.display = "none";
    pop_fram_bk.style.display = "none";
    pop_max_mix = false;
    //如果要记忆窗口内容用上面的，换新的用下面的
    //document.body.removeChild(pop_windiv_obj);
    //pop_windiv_obj = null;
}
//如果调整IE窗口大小则同时调整POP大小
function resizepopwin()
{
    if(pop_windiv_obj != null && pop_max_mix)
    {
        pop_windiv_obj.style.width = document.body.clientWidth - 10;
        pop_windiv_obj.childNodes[1].style.height = document.body.clientHeight - 10 - 22 - 15;
        pop_windiv_obj.style.height = document.body.clientHeight - 10;
        
        pop_fram_bk.style.width = document.body.clientWidth - 10;
    }
}