 
--清理产品数据

--
delete from TB_SEARCH_ProductList where ProductID in(
select pl.ProductID from TB_SEARCH_ProductList as pl
left join TB_SEARCH_Product as pro on pl.ProductID=pro.ProductID
where pro.ProductID is null)
--
delete from TB_SEARCH_Product where ProductID in(
select pl.ProductID from TB_SEARCH_Product as pl
left join   TB_SEARCH_ProductList as pro on pl.ProductID=pro.ProductID
where pro.ProductID is null)
 
 --TRUNCATE TABLE TB_SEARCH_SpiderWorkQueue
 --TRUNCATE TABLE SearchSystemLog..TB_SYS_Logging
 
 select ProductID from
 (
 select pro.ProductID,isnull(pl.ProductID,0) as plid from SearchSystem..TB_SEARCH_ProductList pro 
 left join tb_search_product pl on pro.ProductID = pl.ProductID
 ) as t where t.plid=0
 
 
 /*****************************************************************************************************************
 amazon model
 *****************************************************************************************************************/
 /****** Script for SelectTopNRows command from SSMS  ******/

declare @ProductID int 
declare @Model varchar(50)
declare @str varchar(100)='%Model:%'--	<b>Model</b>: -- OR -- Item model number</td><td class="value">
declare ProductModel cursor for
SELECT  ProductID,SUBSTRING(Specifications,PATINDEX(@str,Specifications)+len(@str)-2,50) as Model  FROM [SearchSystem].[dbo].[TB_SEARCH_Product] 
where	Specifications<>'' and 
		SiteID=1 and 
		isnull(Model,'')='' and  
		PATINDEX(@str,Specifications)>0
open ProductModel
fetch next from ProductModel into @ProductID, @Model
while(@@fetch_status=0)
	begin
		begin
			set @Model=ltrim(@Model)
			if LEN(@Model)>25
				set @Model=SUBSTRING(@Model,1,25)
			if (PATINDEX('%<%',@Model)>0)
				set @Model=SUBSTRING(@Model,1,PATINDEX('%<%',@Model)-1)
			update [SearchSystem].[dbo].[TB_SEARCH_Product]  set Model=@Model where ProductID=@ProductID
		end
	fetch next from ProductModel into @ProductID, @Model
	end		
close ProductModel
deallocate ProductModel 
	
SELECT top 100 ProductID,CategoryID,BrandID,Model,UPCorISBN,SiteID,FullName, SUBSTRING(Specifications,PATINDEX('%<b>Model</b>:%',Specifications),50) as Model,Specifications  FROM [SearchSystem].[dbo].[TB_SEARCH_Product] 
where	Specifications<>'' and 
		SiteID=1 and 
		isnull(Model,'')='' and  
		PATINDEX('%<b>Model</b>:%',Specifications)>0	
		
SELECT top 1000 ProductID,CategoryID,BrandID,Model,UPCorISBN,SiteID,FullName, SUBSTRING(Specifications,PATINDEX('%Item model number</td><td class="value">%',Specifications),500) as Model  FROM [SearchSystem].[dbo].[TB_SEARCH_Product] 
where	Specifications<>'' and 
		SiteID=1 and 
		isnull(Model,'')='' and  
		PATINDEX('%Item model number</td><td class="value">%',Specifications)>0
		
		
		
SELECT top 100 ProductID,CategoryID,BrandID,Model,UPCorISBN,SiteID,FullName, SUBSTRING(Specifications,PATINDEX('%Model:%',Specifications),50) as Model,Specifications  FROM [SearchSystem].[dbo].[TB_SEARCH_Product] 
where	Specifications<>'' and 
		SiteID=1 and 
		isnull(Model,'')='' and  
		PATINDEX('%Model:%',Specifications)>0