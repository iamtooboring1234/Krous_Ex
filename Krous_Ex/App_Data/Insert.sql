INSERT INTO [dbo].[Branches] ([BranchesGUID], [BranchesName], [BranchesAddress], [BranchesEmail], [BranchesTel]) VALUES (N'c44f8b97-b8f9-44e1-94ab-b21d4518cb29', N'KUALA LUMPUR MAIN CAMPUS', N'Jalan Genting Kelang, Setapak,
53300 Kuala Lumpur,
P.O. Box 10979, 50932 Kuala Lumpur, Malaysia.', N'info_krousex@gmail.com', N'60123456789')

INSERT INTO [dbo].[Faculty] ([FacultyGUID], [FacultyAbbrv], [FacultyName], [FacultyDesc]) VALUES (N'c913ca56-1809-4792-b5e7-f3f2b0eebd9c', N'FOCS', N'Faculty of Computer Science ', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus vulputate sed sem vel dapibus. Vestibulum blandit venenatis justo, vitae interdum.')

INSERT INTO [dbo].[Student] ([StudentGUID], [StudentUsername], [StudentPassword], [StudentFullName], [Gender], [DOB], [PhoneNumber], [Email], [NRIC], [YearIntake], [AccountRegisterDate], [BranchesGUID], [FacultyGUID]) VALUES (N'8a44dd59-ec36-4d7c-9c5b-4fd064977d3f', N'jerry', N'dq+YNuNesVRYSlGsF8xMfQ==', N'lau', N'male', N'2013-08-09', N'01126085647', N'krystywoon25@gmail.com', N'001225-14-0368', N'2021', N'2021-10-13 22:20:06', N'c44f8b97-b8f9-44e1-94ab-b21d4518cb29', N'c913ca56-1809-4792-b5e7-f3f2b0eebd9c')
INSERT INTO [dbo].[Student] ([StudentGUID], [StudentUsername], [StudentPassword], [StudentFullName], [Gender], [DOB], [PhoneNumber], [Email], [NRIC], [YearIntake], [AccountRegisterDate], [BranchesGUID], [FacultyGUID]) VALUES (N'b750662c-1bd0-4cca-b5c1-96f60a791f5d', N'krysty12', N'4sp9Jeo2I9tv7+GBJIXCMQ==', N'woon', N'female', N'2001-12-03', N'01126085647', N'krysty25@gmail.com', N'001225-14-0394', N'2021', N'2021-10-14 17:53:07', N'c44f8b97-b8f9-44e1-94ab-b21d4518cb29', N'c913ca56-1809-4792-b5e7-f3f2b0eebd9c')

INSERT INTO [dbo].[Staff] ([StaffGUID], [StaffUsername], [StaffPassword], [StaffFullName], [Gender], [StaffRole], [StaffStatus], [PhoneNumber], [Email], [NRIC], [Specialization], [BranchesGUID], [FacultyGUID]) VALUES (N'cdef4d65-44e1-4737-8da8-9d1b83edba9f', N'jerry5678', N'pojZ9nX5yNS4CWglJ+xm8QsfdByCIsa4ppKY4NS48Fo=', N'Jerry Lau', N'Male', N'Head Admin', N'Active', N'0123456789', N'jerrylau0725@gmail.com', N'000725-10-0739', N'Computer Science', N'c44f8b97-b8f9-44e1-94ab-b21d4518cb29', N'c913ca56-1809-4792-b5e7-f3f2b0eebd9c')
INSERT INTO [dbo].[Staff] ([StaffGUID], [StaffUsername], [StaffPassword], [StaffFullName], [Gender], [StaffRole], [StaffStatus], [PhoneNumber], [Email], [NRIC], [Specialization], [BranchesGUID], [FacultyGUID]) VALUES (N'57bef58f-3b49-4c13-95b4-f53f8974cf1f', N'jerry1234', N'pojZ9nX5yNS4CWglJ+xm8QsfdByCIsa4ppKY4NS48Fo=', N'Jerry Lau', N'Male', N'Head Admin', N'Active', N'0123456789', N'kitty0725gaming@gmail.com', N'000725-10-0739', N'Computer Science', N'c44f8b97-b8f9-44e1-94ab-b21d4518cb29', N'c913ca56-1809-4792-b5e7-f3f2b0eebd9c')

INSERT INTO [dbo].[Discussion] (
[DiscGUID], [ForumGUID], [DiscTopic], [DiscDesc], [DiscStatus], [DiscCreatedBy], [DiscCreatedDate], [DiscLastUpdatedBy], [DiscLastUpdatedDate]) VALUES
(N'57bef58f-3b49-4c13-95b4-f53f8974cf1d', N'8b676194-ee8a-4723-98e8-1be6f37327a4', 'Test', 'Test', 'Test', N'jerry1234', N'2021-10-18 17:17:57', N'jerry1234', N'2021-10-18 17:17:57')

INSERT INTO [dbo].[Replies] (
[ReplyGUID], [DiscGUID], [Reply_Content], [Reply_Date], [Reply_By]) VALUES 
(N'57bef58f-3b49-4c13-95b4-f53f8974cf2d', N'57bef58f-3b49-4c13-95b4-f53f8974cf1d', 'Test', N'2021-10-18 17:17:57', N'jerry1234')

INSERT INTO [dbo].[Replies] (
[ReplyGUID], [DiscGUID], [Reply_Content], [Reply_Date], [Reply_By]) VALUES 
(N'57bef58f-3b49-4c13-95b4-f53f8974cf3d', N'57bef58f-3b49-4c13-95b4-f53f8974cf1d', 'Test', N'2021-10-18 17:17:57', N'jerry1234')

INSERT INTO [dbo].[Replies] (
[ReplyGUID], [DiscGUID], [Reply_Content], [Reply_Date], [Reply_By]) VALUES 
(N'57bef58f-3b49-4c13-95b4-f53f8974cf4d', N'57bef58f-3b49-4c13-95b4-f53f8974cf1d', 'Test', N'2021-10-18 17:17:57', N'jerry1234')

