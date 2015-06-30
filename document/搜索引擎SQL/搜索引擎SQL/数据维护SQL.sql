--update tb_search_product set IsCreateInfoXml=0,IsCreateListXml=0,IsCreateSpecificationsXml=0

--update tb_search_product set IsValid=1 where IsValid=0
--update tb_search_product set IsCheck=1 where IsValid=1

/**价格区间统计**/ 
--select max(MaxPrice),avg(MaxPrice),COUNT(*)as allcount from TB_SEARCH_Product 
--where CategoryID like '26%' and MaxPrice<13.2451

--select max(MaxPrice) as Price from
--(
--	select top (select count(1)/2 from TB_SEARCH_Product where CategoryID like '33%' and IsValid=1 and MaxPrice>0) MaxPrice
--	from TB_SEARCH_Product where CategoryID like '33%' and IsValid=1 and MaxPrice>0 order by MaxPrice
--) as t
--go


/**
合并产品列表
**/

/**根据UPC合并**/
declare ProductUPC cursor for  
	select UPCOrISBN as UPC, coun as ShopCount from 
	(
		select UPCOrISBN,COUNT(*) as coun from 
		(
			select UPCOrISBN from SearchSystem..TB_Search_Product where isnull(UPCOrISBN,'')<>'' group by SiteID,UPCOrISBN
		) as tt group by UPCOrISBN 
	) as tt where tt.coun>1
	
declare @UPC varchar(25)
declare @ShopCount int
declare @MasterProductID int=0

open ProductUPC
fetch next from ProductUPC into @UPC, @ShopCount
while(@@fetch_status=0)
	begin
		begin 
			 select @MasterProductID=ProductID from TB_Search_Product where UPCOrISBN=@UPC and IsValid=1
			 if(@MasterProductID=0)
				begin
					select top 1 @MasterProductID=ProductID from TB_Search_Product as pro left join TB_SEARCH_Site as si on pro.SiteID=si.SiteID  where pro.UPCOrISBN=@UPC  order by si.Rank 
					update TB_Search_Product set IsCheck=1,IsValid=1 where ProductID=@MasterProductID
				end
			if exists(select ProductID from TB_SEARCH_Product where UPCOrISBN=@UPC and IsCheck=0)
				begin
					update TB_SEARCH_ProductList set MergerProductID=@MasterProductID where ProductID in (select ProductID from TB_SEARCH_Product where UPCOrISBN=@UPC and IsCheck=0 )
					update TB_Search_Product set IsCheck=1 where ProductID in (select ProductID from TB_SEARCH_Product where UPCOrISBN=@UPC and IsCheck=0 )
					update TB_Search_Product set IsCreateListXml=0 where ProductID=@MasterProductID
				end
			 
		end
	fetch next from ProductUPC into @UPC, @ShopCount
	end

close ProductUPC
deallocate ProductUPC 

go
/**根据Model合并**/
declare ProductModel cursor for
	select Model, coun as ShopCount from 
	(
		select Model,COUNT(*) as coun from 
		(
			select Model from SearchSystem..TB_Search_Product where isnull(Model,'')<>'' group by SiteID,Model
		) as tt group by Model 
	) as tt where tt.coun>1
	
declare @Model varchar(50)
declare @ShopCount int
declare @MasterProductID int=0

open ProductModel
fetch next from ProductModel into @Model, @ShopCount
while(@@fetch_status=0)
	begin
		begin 
			 select @MasterProductID=ProductID from TB_Search_Product where Model=@Model and IsValid=1
			 if(@MasterProductID=0)
				begin
					select top 1 @MasterProductID=ProductID from TB_Search_Product as pro left join TB_SEARCH_Site as si on pro.SiteID=si.SiteID  where pro.Model=@Model  order by si.Rank 
					update TB_Search_Product set IsCheck=1,IsValid=1 where ProductID=@MasterProductID
				end
			if exists(select ProductID from TB_SEARCH_Product where Model=@Model and IsCheck=0)
				begin
					update TB_SEARCH_ProductList set MergerProductID=@MasterProductID where ProductID in (select ProductID from TB_SEARCH_Product where Model=@Model and IsCheck=0 )
					update TB_Search_Product set IsCheck=1 where ProductID in (select ProductID from TB_SEARCH_Product where Model=@Model and IsCheck=0 )
					update TB_Search_Product set IsCreateListXml=0 where ProductID=@MasterProductID
				end
		end
	fetch next from ProductModel into @Model, @ShopCount
	end

close ProductModel
deallocate ProductModel 

go




/**统计CommentCount ShopCount**/

create table #ProductCommentCount 
(
    ProductID int primary key,   
    CommentCount int 
)

insert #ProductCommentCount 
select productid,SUM(CommentCount) as CommentCount from 
(
	select (case isnull(t2.MergerProductID,0) when 0 then t1.productid else t2.MergerProductID end) as productid,t1.CommentCount
	from (
			select productid,COUNT(1) as CommentCount
			from TB_SEARCH_ProductComment
			group by productid
		 ) as t1 left join tb_search_productlist t2 on t1.ProductID=t2.ProductID
) as ttt group by productid


declare ProductShow cursor for 
select pro.ProductID,isnull(pcc.CommentCount,0) as CommentCount,isnull(ttt.shopCount,0) as ShopCount,ttt.MinPrice,ttt.MaxPrice,ttt.ScoreCount,ttt.Score from TB_SEARCH_Product as pro
left join #ProductCommentCount as pcc on  pcc.ProductID=pro.ProductID
left join 
(
	select productid,COUNT(*) as shopcount,sum(ScoreCount) as ScoreCount,sum(Score) as Score,MAX(MaxPrice) as MaxPrice,MIN(MinPrice) as MinPrice from 
	(
		select (case isnull(MergerProductID,0) when 0 then productid else MergerProductID end) as productid,ScoreCount,Score,
		dbo.GetProductBoundPrice(2,Price,UsedPrice,RentPrice) as MaxPrice,
		dbo.GetProductBoundPrice(1,Price,UsedPrice,RentPrice) as MinPrice
		from tb_search_productlist
	) as tt group by productid
) as ttt on ttt.productid=pro.ProductID


where pro.IsValid=1 

declare @Productid int
declare @CommentCount int
declare @ShopCount int
declare @minPrice money
declare @maxPrice money
declare @ScoreCount int
declare @Score int
open ProductShow
fetch next from ProductShow into @Productid, @CommentCount,@ShopCount,@minPrice,@maxPrice,@ScoreCount,@Score
while(@@fetch_status=0)
	begin
		begin
			 if exists(select * from TB_SEARCH_ProductShow where ProductID=@Productid) 
				update TB_SEARCH_ProductShow set CommentCount=@CommentCount,ShopCount=@ShopCount,minPrice=@minPrice,maxPrice=@maxPrice,ScoreUsers=@ScoreCount,Score=@Score where ProductID=@Productid
			 else
				insert TB_SEARCH_ProductShow(Productid,CommentCount,ShopCount,minPrice,maxPrice,ScoreUsers,Score) values (@Productid, @CommentCount,@ShopCount,@minPrice,@maxPrice,@ScoreCount,@Score)
		end
	fetch next from ProductShow into @Productid, @CommentCount,@ShopCount,@minPrice,@maxPrice,@ScoreCount,@Score
	end
close ProductShow
deallocate ProductShow 
drop table #ProductCommentCount

go

/**
统计分类产品数量 TB_SEARCH_Category.ProductCount
**/
update TB_SEARCH_Category set ProductCount=0

declare ProductStat cursor for 

select cate.CategoryID,
(	
	select COUNT(*) from TB_SEARCH_Product as pro 
	where pro.IsValid=1 and pro.CategoryID like CONVERT(varchar(10),cate.CategoryID)+'%'
) as ProductCount
from TB_SEARCH_Category as cate where cate.IsValid=1

declare @CategoryID int
declare @ProductCount int
open ProductStat
fetch next from ProductStat into @CategoryID, @ProductCount
while(@@fetch_status=0)
	begin
		begin 
			update TB_SEARCH_Category set ProductCount=@ProductCount where CategoryID=@CategoryID
		end
	fetch next from ProductStat into @CategoryID, @ProductCount
	end
close ProductStat
deallocate ProductStat 

go
 
update tb_search_productlist set UrlKey=substring(resourceurl,len(resourceurl)-10,10) where  siteid=1 and UrlKey is null