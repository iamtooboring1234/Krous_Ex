INSERT INTO [dbo].[Branches] ([BranchesGUID], [BranchesName], [BranchesAddress], [BranchesEmail], [BranchesTel]) VALUES (N'c44f8b97-b8f9-44e1-94ab-b21d4518cb29', N'KUALA LUMPUR MAIN CAMPUS', N'Jalan Genting Kelang, Setapak,
53300 Kuala Lumpur,
P.O. Box 10979, 50932 Kuala Lumpur, Malaysia.', N'info_krousex@gmail.com', N'60123456789')

INSERT INTO [dbo].[Faculty] ([FacultyGUID], [FacultyAbbrv], [FacultyName], [FacultyDesc]) VALUES (N'c913ca56-1809-4792-b5e7-f3f2b0eebd9c', N'FOCS', N'Faculty of Computer Science ', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus vulputate sed sem vel dapibus. Vestibulum blandit venenatis justo, vitae interdum.')

INSERT INTO [dbo].[Student] ([StudentGUID], [StudentUsername], [StudentPassword], [StudentFullName], [Gender], [DOB], [PhoneNumber], [Email], [NRIC], [YearIntake], [AccountRegisterDate], [BranchesGUID], [FacultyGUID]) VALUES (N'8a44dd59-ec36-4d7c-9c5b-4fd064977d3f', N'jerry', N'dq+YNuNesVRYSlGsF8xMfQ==', N'lau', N'male', N'2013-08-09', N'01126085647', N'krystywoon25@gmail.com', N'001225-14-0368', N'2021', N'2021-10-13 22:20:06', N'c44f8b97-b8f9-44e1-94ab-b21d4518cb29', N'c913ca56-1809-4792-b5e7-f3f2b0eebd9c')
INSERT INTO [dbo].[Student] ([StudentGUID], [StudentUsername], [StudentPassword], [StudentFullName], [Gender], [DOB], [PhoneNumber], [Email], [NRIC], [YearIntake], [AccountRegisterDate], [BranchesGUID], [FacultyGUID]) VALUES (N'b750662c-1bd0-4cca-b5c1-96f60a791f5d', N'krysty12', N'4sp9Jeo2I9tv7+GBJIXCMQ==', N'woon', N'female', N'2001-12-03', N'01126085647', N'krysty25@gmail.com', N'001225-14-0394', N'2021', N'2021-10-14 17:53:07', N'c44f8b97-b8f9-44e1-94ab-b21d4518cb29', N'c913ca56-1809-4792-b5e7-f3f2b0eebd9c')

INSERT INTO [dbo].[Staff] ([StaffGUID], [StaffUsername], [StaffPassword], [StaffFullName], [Gender], [StaffRole], [StaffStatus], [PhoneNumber], [Email], [NRIC], [Specialization], [BranchesGUID], [FacultyGUID]) VALUES (N'cdef4d65-44e1-4737-8da8-9d1b83edba9f', N'jerry5678', N'pojZ9nX5yNS4CWglJ+xm8QsfdByCIsa4ppKY4NS48Fo=', N'Jerry Lau', N'Male', N'Head Admin', N'Active', N'0123456789', N'jerrylau0725@gmail.com', N'000725-10-0739', N'Computer Science', N'c44f8b97-b8f9-44e1-94ab-b21d4518cb29', N'c913ca56-1809-4792-b5e7-f3f2b0eebd9c')
INSERT INTO [dbo].[Staff] ([StaffGUID], [StaffUsername], [StaffPassword], [StaffFullName], [Gender], [StaffRole], [StaffStatus], [PhoneNumber], [Email], [NRIC], [Specialization], [BranchesGUID], [FacultyGUID]) VALUES (N'57bef58f-3b49-4c13-95b4-f53f8974cf1f', N'jerry1234', N'pojZ9nX5yNS4CWglJ+xm8QsfdByCIsa4ppKY4NS48Fo=', N'Jerry Lau', N'Male', N'Head Admin', N'Active', N'0123456789', N'kitty0725gaming@gmail.com', N'000725-10-0739', N'Computer Science', N'c44f8b97-b8f9-44e1-94ab-b21d4518cb29', N'c913ca56-1809-4792-b5e7-f3f2b0eebd9c')

INSERT INTO [dbo].[Forum] ([ForumGUID], [StaffGUID], [ForumTopic], [ForumDesc], [ForumCategory], [ForumStatus], [ForumType], [ForumCreatedBy], [ForumCreatedDate], [ForumLastUpdatedBy], [ForumLastUpdatedDate]) VALUES (N'8b676194-ee8a-4723-98e8-1be6f37327a4', N'57bef58f-3b49-4c13-95b4-f53f8974cf1f', N'General Discussion', N'Discuss School Related', N'General', N'Active', N'Public', N'jerry1234', N'2021-10-18 17:17:57', N'jerry1234', N'2021-10-18 17:17:57')
INSERT INTO [dbo].[Forum] ([ForumGUID], [StaffGUID], [ForumTopic], [ForumDesc], [ForumCategory], [ForumStatus], [ForumType], [ForumCreatedBy], [ForumCreatedDate], [ForumLastUpdatedBy], [ForumLastUpdatedDate]) VALUES (N'd73d596c-5900-4842-a216-65dedd220f41', N'57bef58f-3b49-4c13-95b4-f53f8974cf1f', N'Announcements ', N'Announcements from school management team', N'Announcements', N'Active', N'Public', N'jerry1234', N'2021-10-20 12:30:52', N'jerry1234', N'2021-10-20 12:30:52')

INSERT INTO [dbo].[Discussion] ([DiscGUID], [ForumGUID], [DiscTopic], [DiscDesc], [DiscContent],[DiscStatus], [DiscIsPinned], [DiscCreatedBy], [DiscCreatedDate], [DiscLastUpdatedBy], [DiscLastUpdatedDate]) VALUES (N'57bef58f-3b49-4c13-95b4-f53f8974cf1d', N'd73d596c-5900-4842-a216-65dedd220f41', N'Test', N'Test', N'Test',N'Active', N'Yes',N'jerry1234', N'2021-10-18 17:17:57', N'jerry1234', N'2021-10-18 17:17:57')
INSERT INTO [dbo].[Discussion] ([DiscGUID], [ForumGUID], [DiscTopic], [DiscDesc], [DiscContent],[DiscStatus], [DiscIsPinned], [DiscCreatedBy], [DiscCreatedDate], [DiscLastUpdatedBy], [DiscLastUpdatedDate]) VALUES (N'57bef58f-3b49-4c13-95b4-f53f8974cc1d', N'd73d596c-5900-4842-a216-65dedd220f41', N'Test', N'Test', N'Test',N'Inactive', N'No',N'jerry1234', N'2021-10-18 17:17:57', N'jerry1234', N'2021-10-18 17:17:57')
INSERT INTO [dbo].[Discussion] ([DiscGUID], [ForumGUID], [DiscTopic], [DiscDesc], [DiscContent],[DiscStatus], [DiscIsPinned], [DiscCreatedBy], [DiscCreatedDate], [DiscLastUpdatedBy], [DiscLastUpdatedDate]) VALUES (N'57bef56f-3b49-4c13-95b4-f53f8974cf1d', N'd73d596c-5900-4842-a216-65dedd220f41', N'Test', N'Test', N'Test',N'Active', N'Yes',N'jerry1234', N'2021-10-18 17:17:57', N'jerry1234', N'2021-10-18 17:17:57')
INSERT INTO [dbo].[Discussion] ([DiscGUID], [ForumGUID], [DiscTopic], [DiscDesc], [DiscContent],[DiscStatus], [DiscIsPinned], [DiscCreatedBy], [DiscCreatedDate], [DiscLastUpdatedBy], [DiscLastUpdatedDate]) VALUES (N'57bef57f-3b49-4c13-95b4-f53f8974cc1d', N'd73d596c-5900-4842-a216-65dedd220f41', N'Test', N'Test', N'Test',N'Inactive', N'No',N'jerry1234', N'2021-10-18 17:17:57', N'jerry1234', N'2021-10-18 17:17:57')

INSERT INTO [dbo].[Replies] ([ReplyGUID], [DiscGUID], [Reply_Content], [Reply_Date], [Reply_By]) VALUES (N'57bef58f-3b49-4c13-95b4-f53f8974cf2d', N'57bef58f-3b49-4c13-95b4-f53f8974cf1d', N'Test', N'2021-10-18 17:17:57', N'jerry1234')
INSERT INTO [dbo].[Replies] ([ReplyGUID], [DiscGUID], [Reply_Content], [Reply_Date], [Reply_By]) VALUES (N'57bef58f-3b49-4c13-95b4-f53f8974cf3d', N'57bef58f-3b49-4c13-95b4-f53f8974cf1d', N'Test', N'2021-10-18 17:17:58', N'jerry1234')
INSERT INTO [dbo].[Replies] ([ReplyGUID], [DiscGUID], [Reply_Content], [Reply_Date], [Reply_By]) VALUES (N'57bef58f-3b49-4c13-95b4-f53f8974cf4d', N'57bef58f-3b49-4c13-95b4-f53f8974cf1d', N'Test', N'2021-10-18 17:17:59', N'jerry1234')
INSERT INTO [dbo].[Replies] ([ReplyGUID], [DiscGUID], [Reply_Content], [Reply_Date], [Reply_By]) VALUES (N'57bef58f-3b49-4c13-95b4-f53f8974cf5d', N'57bef58f-3b49-4c13-95b4-f53f8974cc1d', N'Test', N'2021-10-19 18:17:59', N'jerry1234')

INSERT INTO [dbo].[Programme] ([ProgrammeGUID], [ProgrammeAbbrv], [ProgrammeName], [ProgrammeDesc], [ProgrammeDuration], [ProgrammeCategory], [ProgrammeFullorPart], [ProgrammeFaculty]) VALUES (N'2c21b74b-eedc-4f9a-ab88-038f9f65523d', N'REI', N'Bachelor of Information Systems (Honours) in Enterprise Information Systems', N'Bachelor programme that is related to Enterprise Information Systems', N'3', N'Bachelor Degree', NULL, N'Faculty of Computer Science ')
INSERT INTO [dbo].[Programme] ([ProgrammeGUID], [ProgrammeAbbrv], [ProgrammeName], [ProgrammeDesc], [ProgrammeDuration], [ProgrammeCategory], [ProgrammeFullorPart], [ProgrammeFaculty]) VALUES (N'bd29bb46-0826-498e-b624-1bc9ca5969d5', N'DPIT', N'Doctor of Philosophy (Information Technology) ', N'Provide further studies for Doctor Philosophy in Information Technology which equipped high thinking skills, problem solving skills and research skills.', N'3', N'Doctor of Philosophy', N'Full Time', N'Faculty of Computer Science ')
INSERT INTO [dbo].[Programme] ([ProgrammeGUID], [ProgrammeAbbrv], [ProgrammeName], [ProgrammeDesc], [ProgrammeDuration], [ProgrammeCategory], [ProgrammeFullorPart], [ProgrammeFaculty]) VALUES (N'953802d4-b0af-44c2-ace5-1c82c750dbee', N'DIS', N'Diploma in Information Systems', N'Information System programme', N'2', N'Diploma', NULL, N'Faculty of Computer Science ')
INSERT INTO [dbo].[Programme] ([ProgrammeGUID], [ProgrammeAbbrv], [ProgrammeName], [ProgrammeDesc], [ProgrammeDuration], [ProgrammeCategory], [ProgrammeFullorPart], [ProgrammeFaculty]) VALUES (N'2a328498-7fd2-4a03-85f7-204d3009def1', N'FAC', N'Foundation in Accounting', N'Foundation study in Accounting', N'1', N'Foundation', NULL, N'Faculty of Accountancy, Finance And Business')
INSERT INTO [dbo].[Programme] ([ProgrammeGUID], [ProgrammeAbbrv], [ProgrammeName], [ProgrammeDesc], [ProgrammeDuration], [ProgrammeCategory], [ProgrammeFullorPart], [ProgrammeFaculty]) VALUES (N'f6ac8b7a-ca7f-43f5-b3b7-326e59f6f9f1', N'FSC', N'Foundation in Science', N'Foundation study in Science which covers science-based courses', N'1', N'Foundation', NULL, N'Faculty of Applied Science')
INSERT INTO [dbo].[Programme] ([ProgrammeGUID], [ProgrammeAbbrv], [ProgrammeName], [ProgrammeDesc], [ProgrammeDuration], [ProgrammeCategory], [ProgrammeFullorPart], [ProgrammeFaculty]) VALUES (N'9f8c939a-b5fa-4af0-aadb-335c6bc39ff3', N'DPIT', N'Doctor of Philosophy (Information Technology) ', N'doctors of philosophy in Information Technology', N'4', N'Doctor of Philosophy', N'Part Time', N'Faculty of Computer Science ')
INSERT INTO [dbo].[Programme] ([ProgrammeGUID], [ProgrammeAbbrv], [ProgrammeName], [ProgrammeDesc], [ProgrammeDuration], [ProgrammeCategory], [ProgrammeFullorPart], [ProgrammeFaculty]) VALUES (N'ac6cb816-79b5-4b7d-a049-34910509c551', N'RMM', N'Bachelor of Science (Honours) in Management Mathematics with Computing', N'Management Mathematics with Computing programme that offered to Bachelor Degree student', N'3', N'Bachelor Degree', NULL, N'Faculty of Computer Science ')
INSERT INTO [dbo].[Programme] ([ProgrammeGUID], [ProgrammeAbbrv], [ProgrammeName], [ProgrammeDesc], [ProgrammeDuration], [ProgrammeCategory], [ProgrammeFullorPart], [ProgrammeFaculty]) VALUES (N'7910a8f4-f58e-48df-bcfd-3518a340cd5f', N'DHT', N'Diploma in Hotel Management (DHT)', N'Hotel Management', N'2', N'Diploma', NULL, N'Faculty of Social Science And Humanities')
INSERT INTO [dbo].[Programme] ([ProgrammeGUID], [ProgrammeAbbrv], [ProgrammeName], [ProgrammeDesc], [ProgrammeDuration], [ProgrammeCategory], [ProgrammeFullorPart], [ProgrammeFaculty]) VALUES (N'e135ce99-4928-4262-9871-43749a4d269a', N'RDS', N'Bachelor of Computer Science (Honours) in Data Science', N'Computer Science - Data Science programme', N'3', N'Bachelor Degree', NULL, N'Faculty of Computer Science ')
INSERT INTO [dbo].[Programme] ([ProgrammeGUID], [ProgrammeAbbrv], [ProgrammeName], [ProgrammeDesc], [ProgrammeDuration], [ProgrammeCategory], [ProgrammeFullorPart], [ProgrammeFaculty]) VALUES (N'35065b0a-d5a1-46f3-93c3-439e9c86c29a', N'DFS', N'Diploma in Food Science', N'Programme that covers the knowledge such as chemistry, microbiology, basic food science, food technology and so on. ', N'2', N'Diploma', NULL, N'Faculty of Applied Science')
INSERT INTO [dbo].[Programme] ([ProgrammeGUID], [ProgrammeAbbrv], [ProgrammeName], [ProgrammeDesc], [ProgrammeDuration], [ProgrammeCategory], [ProgrammeFullorPart], [ProgrammeFaculty]) VALUES (N'316d2631-caf5-40b5-9b5f-44d321cf2d9f', N'DIT', N'Diploma in Information Technology', N'Information technology programme. This programme will covered C language, Assembly language, HTML5, CSS and so on.', N'2', N'Diploma', NULL, N'Faculty of Computer Science ')
INSERT INTO [dbo].[Programme] ([ProgrammeGUID], [ProgrammeAbbrv], [ProgrammeName], [ProgrammeDesc], [ProgrammeDuration], [ProgrammeCategory], [ProgrammeFullorPart], [ProgrammeFaculty]) VALUES (N'abbc8e25-d9b3-4a1b-b3d5-4ef626bdadd9', N'MCS', N'Master of Computer Science', N'master of computer science - part time', N'3', N'Master', N'Part Time', N'Faculty of Computer Science ')
INSERT INTO [dbo].[Programme] ([ProgrammeGUID], [ProgrammeAbbrv], [ProgrammeName], [ProgrammeDesc], [ProgrammeDuration], [ProgrammeCategory], [ProgrammeFullorPart], [ProgrammeFaculty]) VALUES (N'28630e63-31c2-4418-9297-5d389cc62f0f', N'RSD', N'Bachelor of Information Technology (Honours) in Software System Development', N'It''s a Bachelor Degree programme that covered different course such as Web Application Development, Human Computer Interaction, Computer Networks and so on. There is also Elective Course to be offered such as Principle of Accounting, Internet of Things, Mobile Application Development and so on.', N'3', N'Bachelor Degree', NULL, N'Faculty of Computer Science ')
INSERT INTO [dbo].[Programme] ([ProgrammeGUID], [ProgrammeAbbrv], [ProgrammeName], [ProgrammeDesc], [ProgrammeDuration], [ProgrammeCategory], [ProgrammeFullorPart], [ProgrammeFaculty]) VALUES (N'3beebb5a-5c92-4b06-8393-5e37d7dc26ba', N'RSF', N'Bachelor of Computer Science (Honours) in Software Engineering', N'Software Engineering programme ', N'3', N'Bachelor Degree', NULL, N'Faculty of Computer Science ')
INSERT INTO [dbo].[Programme] ([ProgrammeGUID], [ProgrammeAbbrv], [ProgrammeName], [ProgrammeDesc], [ProgrammeDuration], [ProgrammeCategory], [ProgrammeFullorPart], [ProgrammeFaculty]) VALUES (N'41f8187c-6a92-47b5-9321-713de4b4fe11', N'MCS', N'Master of Computer Science', N'Master of computer science programme', N'2', N'Master', N'Full Time', N'Faculty of Computer Science ')
INSERT INTO [dbo].[Programme] ([ProgrammeGUID], [ProgrammeAbbrv], [ProgrammeName], [ProgrammeDesc], [ProgrammeDuration], [ProgrammeCategory], [ProgrammeFullorPart], [ProgrammeFaculty]) VALUES (N'e0c7357a-fd49-46c6-8357-83d2429fc4dd', N'DBF', N'Diploma in Banking and Finance', N'Banking and Finance programme for diploma student', N'2', N'Diploma', NULL, N'Faculty of Accountancy, Finance And Business')
INSERT INTO [dbo].[Programme] ([ProgrammeGUID], [ProgrammeAbbrv], [ProgrammeName], [ProgrammeDesc], [ProgrammeDuration], [ProgrammeCategory], [ProgrammeFullorPart], [ProgrammeFaculty]) VALUES (N'8d82569a-987d-4ab9-b2e2-91a399aa287b', N'RAC', N'Bachelor of Accounting (Honours)', N'Accounting studies programme', N'4', N'Bachelor Degree', NULL, N'Faculty of Accountancy, Finance And Business')
INSERT INTO [dbo].[Programme] ([ProgrammeGUID], [ProgrammeAbbrv], [ProgrammeName], [ProgrammeDesc], [ProgrammeDuration], [ProgrammeCategory], [ProgrammeFullorPart], [ProgrammeFaculty]) VALUES (N'3e0e6f65-5a11-4a54-8d20-96080a10b64b', N'RCA', N'Bachelor of Corporate Administration (Honours)  ', N'Corporate Administration Programme', N'3', N'Bachelor Degree', NULL, N'Faculty of Accountancy, Finance And Business')
INSERT INTO [dbo].[Programme] ([ProgrammeGUID], [ProgrammeAbbrv], [ProgrammeName], [ProgrammeDesc], [ProgrammeDuration], [ProgrammeCategory], [ProgrammeFullorPart], [ProgrammeFaculty]) VALUES (N'9058a9d1-2112-4630-a503-96d97829e75a', N'DPFS', N'Doctor of Philosophy (Food Science)', N'Further study of Doctor philosophy in Food Science', N'3', N'Doctor of Philosophy', N'Full Time', N'Faculty of Applied Science')
INSERT INTO [dbo].[Programme] ([ProgrammeGUID], [ProgrammeAbbrv], [ProgrammeName], [ProgrammeDesc], [ProgrammeDuration], [ProgrammeCategory], [ProgrammeFullorPart], [ProgrammeFaculty]) VALUES (N'73ec8778-4081-4ef8-a0b1-a07fce4294d6', N'DSE', N'Diploma in Software Engineering', N'Software Engineering (Diploma)', N'2', N'Diploma', NULL, N'Faculty of Computer Science ')
INSERT INTO [dbo].[Programme] ([ProgrammeGUID], [ProgrammeAbbrv], [ProgrammeName], [ProgrammeDesc], [ProgrammeDuration], [ProgrammeCategory], [ProgrammeFullorPart], [ProgrammeFaculty]) VALUES (N'3ab9cf83-d71b-41df-b9ea-aa269b55fff9', N'DPMS', N'Doctor of Philosophy (Mathematical Sciences)', N'Further studies of Doctor Philosophy in Mathematical Sciences', N'3', N'Doctor of Philosophy', N'Full Time', N'Faculty of Computer Science ')
INSERT INTO [dbo].[Programme] ([ProgrammeGUID], [ProgrammeAbbrv], [ProgrammeName], [ProgrammeDesc], [ProgrammeDuration], [ProgrammeCategory], [ProgrammeFullorPart], [ProgrammeFaculty]) VALUES (N'42653863-c594-4d68-b64a-cee41f7c9dd6', N'DPFS', N'Doctor of Philosophy (Food Science)', N'Food Science - Part Time - 4 years - Doctor of Philosophy', N'4', N'Doctor of Philosophy', N'Part Time', N'Faculty of Applied Science')
INSERT INTO [dbo].[Programme] ([ProgrammeGUID], [ProgrammeAbbrv], [ProgrammeName], [ProgrammeDesc], [ProgrammeDuration], [ProgrammeCategory], [ProgrammeFullorPart], [ProgrammeFaculty]) VALUES (N'e8fb42c1-a4ed-4f0d-862a-d0c4ef2eea63', N'MFS', N'Master of Science (Food Science)', N'master of food science - part time', N'3', N'Master', N'Part Time', N'Faculty of Applied Science')
INSERT INTO [dbo].[Programme] ([ProgrammeGUID], [ProgrammeAbbrv], [ProgrammeName], [ProgrammeDesc], [ProgrammeDuration], [ProgrammeCategory], [ProgrammeFullorPart], [ProgrammeFaculty]) VALUES (N'7d3ca129-b13b-4b66-baa5-d96f5deeafd3', N'MFS', N'Master of Science (Food Science)', N'master of food science - full time', N'2', N'Master', N'Full Time', N'Faculty of Applied Science')
INSERT INTO [dbo].[Programme] ([ProgrammeGUID], [ProgrammeAbbrv], [ProgrammeName], [ProgrammeDesc], [ProgrammeDuration], [ProgrammeCategory], [ProgrammeFullorPart], [ProgrammeFaculty]) VALUES (N'032a0c91-d0db-4b99-a966-da3a2164fc96', N'FBS', N'Foundation in Business', N'Foundation study in Business ', N'1', N'Foundation', NULL, N'Faculty of Accountancy, Finance And Business')
INSERT INTO [dbo].[Programme] ([ProgrammeGUID], [ProgrammeAbbrv], [ProgrammeName], [ProgrammeDesc], [ProgrammeDuration], [ProgrammeCategory], [ProgrammeFullorPart], [ProgrammeFaculty]) VALUES (N'b5b28f2c-1115-4229-a51d-df857e1c4467', N'RBF', N'Bachelor of Banking and Finance (Honours)', N'Banking and Finance', N'3', N'Bachelor Degree', NULL, N'Faculty of Accountancy, Finance And Business')
INSERT INTO [dbo].[Programme] ([ProgrammeGUID], [ProgrammeAbbrv], [ProgrammeName], [ProgrammeDesc], [ProgrammeDuration], [ProgrammeCategory], [ProgrammeFullorPart], [ProgrammeFaculty]) VALUES (N'12f934f0-a5f6-46e2-9f71-e10351c701f4', N'MSMS', N'Master of Science in Mathematical Sciences', N'master of science in mathematical sciences - full time', N'2', N'Master', N'Full Time', N'Faculty of Computer Science ')
INSERT INTO [dbo].[Programme] ([ProgrammeGUID], [ProgrammeAbbrv], [ProgrammeName], [ProgrammeDesc], [ProgrammeDuration], [ProgrammeCategory], [ProgrammeFullorPart], [ProgrammeFaculty]) VALUES (N'9baa8a13-9a51-4dfa-809b-e218a37c25f4', N'MSMS', N'Master of Science in Mathematical Sciences', N'master of science in mathematical sciences - part time', N'3', N'Master', N'Part Time', N'Faculty of Computer Science ')
INSERT INTO [dbo].[Programme] ([ProgrammeGUID], [ProgrammeAbbrv], [ProgrammeName], [ProgrammeDesc], [ProgrammeDuration], [ProgrammeCategory], [ProgrammeFullorPart], [ProgrammeFaculty]) VALUES (N'69dc85da-59b5-4c86-a2f9-e248373cb68d', N'RIT', N'Bachelor of Information Technology (Honours) in Internet Technology', N'Internet Technology Programme', N'3', N'Bachelor Degree', NULL, N'Faculty of Computer Science ')
INSERT INTO [dbo].[Programme] ([ProgrammeGUID], [ProgrammeAbbrv], [ProgrammeName], [ProgrammeDesc], [ProgrammeDuration], [ProgrammeCategory], [ProgrammeFullorPart], [ProgrammeFaculty]) VALUES (N'7784fd37-411d-4972-97dc-e6efb83c56d5', N'FCP', N'Foundation in Computing', N'Foundation study in Computing which covers basic IT related courses that provide the student a solid foundation of the course.', N'1', N'Foundation', NULL, N'Faculty of Computer Science ')
INSERT INTO [dbo].[Programme] ([ProgrammeGUID], [ProgrammeAbbrv], [ProgrammeName], [ProgrammeDesc], [ProgrammeDuration], [ProgrammeCategory], [ProgrammeFullorPart], [ProgrammeFaculty]) VALUES (N'cc751f26-8d28-44b1-b03a-ef69b4b319ce', N'DPMS', N'Doctor of Philosophy (Mathematical Sciences)', N'further studies of doctor philosophy in Mathematical Science', N'4', N'Doctor of Philosophy', N'Part Time', N'Faculty of Computer Science ')
