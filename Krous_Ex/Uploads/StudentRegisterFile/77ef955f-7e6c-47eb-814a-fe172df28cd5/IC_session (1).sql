CREATE TABLE [dbo].[Session] (
    [SessionGUID] UNIQUEIDENTIFIER    NOT NULL,
    [SessionYear] INT NOT NULL,
    [SessionMonth] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([SessionGUID])
);

INSERT INTO [dbo].[Session] ([SessionGUID], [SessionYear], [SessionMonth]) VALUES (N'aae03e7e-045b-43a1-8a94-194599350ada', 2019, 1)
INSERT INTO [dbo].[Session] ([SessionGUID], [SessionYear], [SessionMonth]) VALUES (N'4b911ed8-916b-42f7-b747-2ec0bb3a4a53', 2018, 5)
INSERT INTO [dbo].[Session] ([SessionGUID], [SessionYear], [SessionMonth]) VALUES (N'8f7decad-e43e-4947-8fcc-471117b1d07a', 2018, 9)
INSERT INTO [dbo].[Session] ([SessionGUID], [SessionYear], [SessionMonth]) VALUES (N'0afce354-fd9d-4cf8-a649-4ed22cdb4855', 2018, 1)
INSERT INTO [dbo].[Session] ([SessionGUID], [SessionYear], [SessionMonth]) VALUES (N'4b45bed0-7615-4190-a71c-55347cbfa0ea', 2020, 9)
INSERT INTO [dbo].[Session] ([SessionGUID], [SessionYear], [SessionMonth]) VALUES (N'ca069eb2-cff7-4ff5-8a71-641ac90ee728', 2022, 9)
INSERT INTO [dbo].[Session] ([SessionGUID], [SessionYear], [SessionMonth]) VALUES (N'd2938bc4-d11d-4fd7-acaa-674e01281cf7', 2016, 1)
INSERT INTO [dbo].[Session] ([SessionGUID], [SessionYear], [SessionMonth]) VALUES (N'431b8d90-ddf2-4e8b-b13f-7d0ddd6bc966', 2020, 1)
INSERT INTO [dbo].[Session] ([SessionGUID], [SessionYear], [SessionMonth]) VALUES (N'99e68b1d-9151-4209-ac96-844cc4168dc0', 2021, 9)
INSERT INTO [dbo].[Session] ([SessionGUID], [SessionYear], [SessionMonth]) VALUES (N'dbe19259-5228-47ee-8cde-86344f5d1f74', 2021, 1)
INSERT INTO [dbo].[Session] ([SessionGUID], [SessionYear], [SessionMonth]) VALUES (N'2564f0a6-6e77-4124-b881-8bc037b99ee0', 2016, 9)
INSERT INTO [dbo].[Session] ([SessionGUID], [SessionYear], [SessionMonth]) VALUES (N'c5909910-f820-47f7-9d07-97e346816942', 2016, 5)
INSERT INTO [dbo].[Session] ([SessionGUID], [SessionYear], [SessionMonth]) VALUES (N'fd3e8064-0223-4a43-8265-a140ef6b5bab', 2017, 9)
INSERT INTO [dbo].[Session] ([SessionGUID], [SessionYear], [SessionMonth]) VALUES (N'e53caa83-4573-4dc1-bea7-c7c01a4b2e3a', 2022, 5)
INSERT INTO [dbo].[Session] ([SessionGUID], [SessionYear], [SessionMonth]) VALUES (N'dd26db64-caa1-4edd-b12b-d2b2713471ce', 2019, 5)
INSERT INTO [dbo].[Session] ([SessionGUID], [SessionYear], [SessionMonth]) VALUES (N'b74961ab-5fcd-4dba-a069-eab969745f18', 2021, 5)
INSERT INTO [dbo].[Session] ([SessionGUID], [SessionYear], [SessionMonth]) VALUES (N'bfe76c55-3c7c-41b3-9d40-f119bd60b4f2', 2017, 1)
INSERT INTO [dbo].[Session] ([SessionGUID], [SessionYear], [SessionMonth]) VALUES (N'5da74545-0324-4006-9198-f1a23c26cdd2', 2020, 5)
INSERT INTO [dbo].[Session] ([SessionGUID], [SessionYear], [SessionMonth]) VALUES (N'7c0f5120-729e-4183-90c7-f29aa0fef73a', 2017, 5)
INSERT INTO [dbo].[Session] ([SessionGUID], [SessionYear], [SessionMonth]) VALUES (N'0e7b8941-28d2-4fe0-ae46-f364a84fc455', 2019, 9)
INSERT INTO [dbo].[Session] ([SessionGUID], [SessionYear], [SessionMonth]) VALUES (N'1f9dc3a7-c11c-4737-91f4-fa4848fa4f14', 2022, 1)

/*Get new session - two weeks in advance*/
select * from session 
WHERE DATEADD(Day, 14, GETDATE()) < convert(varchar, concat(sessionYear, '-',SessionMonth, '-', DAY(getdate())), 22) 
order by SessionYear, SessionMonth;

/*Get old session*/
select * from session WHERE GETDATE() > convert(varchar, concat(sessionYear, '-',SessionMonth, '-', DAY(getdate())), 22) order by SessionYear, SessionMonth;