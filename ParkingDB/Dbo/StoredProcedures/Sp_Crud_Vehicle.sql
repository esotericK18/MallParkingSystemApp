--===================================================================================
--GET ALL					-> @init_flag = 0
--GET BY ID					-> @init_flag = 1
--INSERT					-> @init_flag = 2
--UPDATE					-> @init_flag = 3
--DELETE					-> @init_flag = 4
--GET BY NAME(PLATE #)		-> @init_flag = 5

--===================================================================================
CREATE PROCEDURE [dbo].[Sp_Crud_Vehicle]
(
     @init_flag		int
	,@Id			int		        = NULL
	,@PlateNumber	VARCHAR(20)	    = NULL
	,@Size	        int	            = NULL
)
AS

DECLARE @ERR		int
DECLARE @TABLE_NAME varchar(20)
DECLARE @RESULT		int

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	-- =================================================================================
	-- 		GET ALL
	-- =================================================================================
	IF @init_flag = 0
	BEGIN
		SELECT *
		FROM [dbo].[Vehicle]
	END	

	-- =================================================================================
	-- 		GET BY ID
	-- =================================================================================
	ELSE IF @init_flag = 1
	BEGIN
		
		SELECT TOP(1) *
		FROM [dbo].[Vehicle]
		WHERE [Id] = @Id
	END	

	-- =================================================================================
	--		INSERT	
	-- =================================================================================	
	ELSE IF @init_flag = 2
	BEGIN
		INSERT INTO [dbo].[Vehicle]
		(	
			 [PlateNumber]
            ,[Size]
		) 
		VALUES 
		(	
			 @PlateNumber
            ,@Size
		)
	END
		
	
	-- =================================================================================
	-- 		UPDATE
	-- =================================================================================
	ELSE IF @init_flag = 3
	BEGIN	
		UPDATE [dbo].[Vehicle] SET
			 [PlateNumber]  = @PlateNumber
            ,[Size]         = @Size
		WHERE 
			[Id] = @Id	
	END


	-- =================================================================================
	-- 		DELETE
	-- =================================================================================
	ELSE IF @init_flag = 4
	BEGIN
		DELETE
		FROM [dbo].[Vehicle]
		WHERE [Id] = @Id
	
	END
    
    -- =================================================================================
	-- 		GET BY NAME
	-- =================================================================================
	ELSE IF @init_flag = 5
	BEGIN
		
		SELECT TOP(1) *
		FROM [dbo].[Vehicle]
		WHERE [PlateNumber] = @PlateNumber
	END	

	RETURN 0		
END

GO


