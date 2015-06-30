use SearchSystem 
select count(*) from TB_SEARCH_SiteCategory where SiteID=500
select * from TB_SEARCH_SiteCategory where SiteID=808 and CategoryID like '17%' order by CategoryID desc

select * from TB_SEARCH_Category  where ProductCount>0

select  count(*) from TB_SEARCH_ProductList with(nolock) where siteid=30


select  top 10 * from TB_SEARCH_ProductList where LEN(categoryids)>10  

select top 1000 * from tb_search_hotword

select top 10 * from TB_SEARCH_ProductList where siteid=30 order by OrderID desc



select siteid,count(*) from TB_SEARCH_Product group by siteid

select * from tb_search_category where categoryname like '%VoIP%' and Isvalid=1

select * from tb_search_category where categoryid like '29%' order by categoryid


select * from TB_SEARCH_SiteConfig

/**
select Model  , coun as ShopCount from 
	(
		select Model,COUNT(*) as coun from 
		(
			select Model from SearchSystem..TB_Search_Product where isnull(Model,'')<>'' group by SiteID,Model
		) as tt group by Model 
	) as tt where tt.coun>1

select COUNT(*) from TB_SEARCH_Product where   isnull(Model,'')<>'' and siteid=1

SELECT top 1000 ProductID,Model,SUBSTRING(Specifications,PATINDEX('%Item model number</td><td class="value">%',Specifications),500) as Model  FROM [SearchSystem].[dbo].[TB_SEARCH_Product] 
where	Specifications<>'' and 
		SiteID=1 and 
		 
		PATINDEX('%Item model number</td><td class="value">%',Specifications)>0
		
SELECT count(*) FROM [SearchSystem].[dbo].[TB_SEARCH_Product] 
where	Specifications<>'' and 
		SiteID=1 and 
		isnull(Model,'')='' and  
		PATINDEX('%Model:%',Specifications)>0		

**/

select COUNT(*) from TB_SEARCH_Product where IsCheck=1 and IsValid=0

select count(*) from tb_search_productlist  where  siteid=1 and isnull(UrlKey,'')=''