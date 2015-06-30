select getdate()

/**
select count(*) from SearchSystem..TB_Search_ProductList
select count(*) from SearchSystem..TB_Search_ProductList with(nolock) where siteid=1
--2014-01-08 17:49:12.470  14230
SELECT count(*)   FROM [SearchSystem].[dbo].[TB_SEARCH_Product] where categoryid like '33%' 
//时间						总数
//2013-12-27 23:13:04.800   2349708
//2013-12-28 14:57:23.360	2360936
//2013-12-30 13:57:03.630   2392667
//2014-01-03 09:39:57.983	2483293
select count(*) from SearchSystem..TB_Search_ProductList where UrlKey is null

select top 10 *  from SearchSystem..TB_Search_ProductList order by orderid desc
select count(*)  from SearchSystem..TB_Search_Product 
select count(*)  from SearchSystem..TB_Search_Product where UPCOrISBN=''
select count(*)  from SearchSystem..TB_Search_Product where isnull(UPCOrISBN,'')=''
select count(*)  from SearchSystem..TB_Search_Product where len(UPCOrISBN)>5 
--1445274  2014-01-05 20:57:39.527


//2013-11-27 23:44 Count=2349309  Max(ordid)=2666348
select top 1 * from SearchSystem..TB_Search_ProductComment order by ordid desc

select count(*) from SearchSystem..TB_Search_ProductComment where ordid<=2666348
select count(*) from SearchSystem..TB_Search_ProductComment where ordid>2666348

select count(*) from SearchSystem..TB_Search_ProductComment where Comment=''
//时间							ordid<=2666348	ordid>2666348
//2014-01-20 06:54:59.407		1291943			1380152

select count(*) from SearchSystem..TB_Search_ProductComment with(nolock) where siteid=30
 
//2013-12-28	
1720162
794032
//
//2014-01-03 09:39:57.983
1591151
1140834

select * from 
(
select productid from SearchSystem..TB_Search_ProductComment where ordid>2666348 group by productid
) as t,
 
(
select productid from SearchSystem..TB_Search_ProductComment where ordid<=2666348 group by productid
) as t1 where t.productid=t1.productid 

select * from SearchSystem..TB_Search_ProductComment where ordid<=2666348  and productid in (select productid from SearchSystem..TB_Search_ProductComment where ordid>2666348 group by productid)

select * from SearchSystem..TB_Search_ProductComment where  productid=100002511

  
  
select productid from SearchSystem..TB_Search_ProductComment where ordid>2666348 group by productid


select * from searchSystemLog..tb_SEM_PointLog 
where DVID<>'635204778604151971343' and 
      UserIP<>'180.166.175.114' and 
      logid>2032 and 
      dvid<>'635204358822079406628' and ThisAccessTime>'2014-01-09' and ReferrerWord<>'' and referrerSite<>'www.digview.com'





select categoryid,word from 
(
	select  Convert(int,substring(Convert(varchar(10),categoryid),1,2)) as categoryid,word from TB_SEARCH_HotWord where status=1  
) as t group by categoryid,word


select tt.ProductID,tt.ResourceUrl,t.cc from (
SELECT ProductID,count(*)  as cc FROM [SearchSystem].[dbo].[TB_SEARCH_ProductComment]  group by [ProductID] ) t 
  left join SearchSystem..TB_Search_ProductList as tt on tt.[ProductID]=t.[ProductID]
where t.cc<10 


 --truncate table SearchSystemLog..TB_Sys_Logging

select word,remark from tb_search_hotword group by word,remark



select top 100  pro.ProductID,pro.CategoryID,pro.FullName,pro.ImageType,pro.Model,pro.UPCorISBN,pro.SiteID,pro.Description,pro.ShopCount,pro.MaxPrice,pro.MinPrice,pro.BrandID,
		pl.ResourceUrl,  
		isnull(ps.Score,0) as Score, isnull(ps.ScoreUsers,0) as ScoreUsers,isnull(ps.CommentCount,0) as  CommentCount ,		
		ISNULL(ttt.Discount,0) as Discount ,isnull(ttt.OrgPrice,0) as OrgPrice ,	 
		bd.BrandName 
from tb_search_product as pro 
	left join tb_search_productlist as pl on pro.productid=pl.productid 
	left join tb_search_productshow as ps on pro.productid = ps.productid  
	left join (	
				select ProductID,max(Discount) as Discount,max(OrgPrice) as OrgPrice 
				from ( 
						select (CASE WHEN MergerProductID>0 THEN MergerProductID ELSE ProductID END) as ProductID,(OrgPrice-Price)/OrgPrice as Discount,OrgPrice 
						from tb_search_productlist where OrgPrice>Price 
					 ) as tt 
				group by ProductID
			  ) as ttt on ttt.ProductID=pro.ProductID 
	left join TB_SEARCH_Brand bd on pro.BrandID=bd.BrandID  
where pro.categoryid like '1701%' and pro.MinPrice>100 
order by Discount desc





**/

 


 select count(*) from SearchSystem..TB_SEARCH_Product where CategoryID like '12%'