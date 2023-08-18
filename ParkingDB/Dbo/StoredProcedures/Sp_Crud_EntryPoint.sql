--===================================================================================
--GET ALL					-> @init_flag = 0
--GET BY ID					-> @init_flag = 1
--INSERT					-> @init_flag = 2
--UPDATE					-> @init_flag = 3
--DELETE					-> @init_flag = 4
--GET BY NAME       		-> @init_flag = 5
--===================================================================================
CREATE PROCEDURE [dbo].[Sp_Crud_EntryPoint]
(
     @init_flag		int
	,@Id			int				= NULL
	,@Name			NVARCHAR(20)	= NULL
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
		FROM [dbo].[EntryPoint]
	END	

	-- =================================================================================
	-- 		GET BY ID
	-- =================================================================================
	ELSE IF @init_flag = 1
	BEGIN
		
		SELECT TOP(1) *
		FROM [dbo].[EntryPoint]
		WHERE [Id] = @Id
	END	

	-- =================================================================================
	--		INSERT	
	-- =================================================================================	
	ELSE IF @init_flag = 2
	BEGIN
		INSERT INTO [dbo].[EntryPoint]
		(	
			[Name]
		) 
		VALUES 
		(	
			@Name			
		)
	END
		
	
	-- =================================================================================
	-- 		UPDATE
	-- =================================================================================
	ELSE IF @init_flag = 3
	BEGIN	
		UPDATE [dbo].[EntryPoint] SET
			[Name] 		 = @Name      
		WHERE 
			[Id] = @Id	
	END


	-- =================================================================================
	-- 		DELETE
	-- =================================================================================
	ELSE IF @init_flag = 4
	BEGIN
		DELETE
		FROM [dbo].[EntryPoint]
		WHERE [Id] = @Id
	
	END	

    -- =================================================================================
	-- 		GET BY NAME
	-- =================================================================================
	ELSE IF @init_flag = 5
	BEGIN
		
		SELECT TOP(1) *
		FROM [dbo].[EntryPoint]
		WHERE [Name] = @Name
	END	

	RETURN 0		
END

GO


