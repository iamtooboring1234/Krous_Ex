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

INSERT INTO [dbo].[Discussion] ([DiscGUID], [ForumGUID], [DiscTopic], [DiscDesc], [DiscContent], [DiscStatus], [DiscIsPinned], [DiscIsLocked], [DiscCreatedBy], [DiscCreatedDate], [DiscLastUpdatedBy], [DiscLastUpdatedDate]) VALUES (N'8bfccfbf-863b-4e09-83c4-76e4ad2d0e96', N'8b676194-ee8a-4723-98e8-1be6f37327a4', N'Test 4', N'Test 4', N'Test 4', N'Active', N'No', N'No',N'jerry', N'2021-10-20 19:44:20', N'jerry', N'2021-10-20 19:44:20')
INSERT INTO [dbo].[Discussion] ([DiscGUID], [ForumGUID], [DiscTopic], [DiscDesc], [DiscContent], [DiscStatus], [DiscIsPinned], [DiscIsLocked], [DiscCreatedBy], [DiscCreatedDate], [DiscLastUpdatedBy], [DiscLastUpdatedDate]) VALUES (N'b3063419-2f0c-4709-bcf5-81d3aaf11d98', N'8b676194-ee8a-4723-98e8-1be6f37327a4', N'Test 5', N'Test 5', N'Test 5', N'Active', N'No', N'No',N'jerry5678', N'2021-10-20 20:37:24', N'jerry5678', N'2021-10-20 20:37:24')
INSERT INTO [dbo].[Discussion] ([DiscGUID], [ForumGUID], [DiscTopic], [DiscDesc], [DiscContent], [DiscStatus], [DiscIsPinned], [DiscIsLocked], [DiscCreatedBy], [DiscCreatedDate], [DiscLastUpdatedBy], [DiscLastUpdatedDate]) VALUES (N'57bef58f-3b49-4c13-95b4-f53f8974cc1d', N'8b676194-ee8a-4723-98e8-1be6f37327a4', N'Test', N'Test', N'Test', N'Inactive', N'No', N'No',N'jerry1234', N'2021-10-18 17:17:58', N'jerry1234', N'2021-10-18 17:17:57')
INSERT INTO [dbo].[Discussion] ([DiscGUID], [ForumGUID], [DiscTopic], [DiscDesc], [DiscContent], [DiscStatus], [DiscIsPinned], [DiscIsLocked], [DiscCreatedBy], [DiscCreatedDate], [DiscLastUpdatedBy], [DiscLastUpdatedDate]) VALUES (N'57bef56f-3b49-4c13-95b4-f53f8974cf1d', N'd73d596c-5900-4842-a216-65dedd220f41', N'Test', N'Test', N'Test', N'Active', N'Yes', N'Yes',N'jerry1234', N'2021-10-18 17:17:59', N'jerry1234', N'2021-10-18 17:17:57')
INSERT INTO [dbo].[Discussion] ([DiscGUID], [ForumGUID], [DiscTopic], [DiscDesc], [DiscContent], [DiscStatus], [DiscIsPinned], [DiscIsLocked], [DiscCreatedBy], [DiscCreatedDate], [DiscLastUpdatedBy], [DiscLastUpdatedDate]) VALUES (N'57bef58f-3b49-4c13-95b4-f53f8974cf1d', N'8b676194-ee8a-4723-98e8-1be6f37327a4', N'Test', N'Test', N'This is a discussion content', N'Active', N'Yes', N'Yes',N'jerry1234', N'2021-10-18 17:17:55', N'jerry1234', N'2021-10-18 17:17:57')
INSERT INTO [dbo].[Discussion] ([DiscGUID], [ForumGUID], [DiscTopic], [DiscDesc], [DiscContent], [DiscStatus], [DiscIsPinned], [DiscIsLocked], [DiscCreatedBy], [DiscCreatedDate], [DiscLastUpdatedBy], [DiscLastUpdatedDate]) VALUES (N'ce20338a-ccb9-4803-b3d3-ff59cf27a3fc', N'd73d596c-5900-4842-a216-65dedd220f41', N'Test 3', N'Test 3', N'Test 3', N'Active', N'No', N'No',N'jerry1234', N'2021-10-20 18:17:14', N'jerry1234', N'2021-10-20 18:17:14')

INSERT INTO [dbo].[Replies] ([ReplyGUID], [DiscGUID], [ReplyContent], [ReplyDate], [ReplyBy]) VALUES (N'eecd1ef6-57a8-4cb0-b405-14443f87a4d9', N'57bef58f-3b49-4c13-95b4-f53f8974cf1d', N'test 1', N'2021-10-19 22:12:23', N'jerry1234')
INSERT INTO [dbo].[Replies] ([ReplyGUID], [DiscGUID], [ReplyContent], [ReplyDate], [ReplyBy]) VALUES (N'6771fb51-b1b0-486a-8f7c-4e90a4a450eb', N'ce20338a-ccb9-4803-b3d3-ff59cf27a3fc', N'Lets Go', N'2021-10-20 18:21:38', N'jerry')
INSERT INTO [dbo].[Replies] ([ReplyGUID], [DiscGUID], [ReplyContent], [ReplyDate], [ReplyBy]) VALUES (N'c4fdbf48-f8b8-45c3-bf65-dcb8cfef2b61', N'57bef58f-3b49-4c13-95b4-f53f8974cf1d', N'asd', N'2021-10-19 22:20:29', N'jerry')
INSERT INTO [dbo].[Replies] ([ReplyGUID], [DiscGUID], [ReplyContent], [ReplyDate], [ReplyBy]) VALUES (N'39da2ba7-470f-4f63-891d-f475c5ee41d6', N'ce20338a-ccb9-4803-b3d3-ff59cf27a3fc', N'Lets go', N'2021-10-20 18:21:08', N'jerry1234')
INSERT INTO [dbo].[Replies] ([ReplyGUID], [DiscGUID], [ReplyContent], [ReplyDate], [ReplyBy]) VALUES (N'57bef58f-3b49-4c13-95b4-f53f8974cf2d', N'57bef58f-3b49-4c13-95b4-f53f8974cf1d', N'Rpely 1', N'2021-10-18 17:17:57', N'jerry1234')
INSERT INTO [dbo].[Replies] ([ReplyGUID], [DiscGUID], [ReplyContent], [ReplyDate], [ReplyBy]) VALUES (N'57bef58f-3b49-4c13-95b4-f53f8974cf3d', N'57bef58f-3b49-4c13-95b4-f53f8974cf1d', N'Reply 2', N'2021-10-18 17:17:58', N'jerry1234')
INSERT INTO [dbo].[Replies] ([ReplyGUID], [DiscGUID], [ReplyContent], [ReplyDate], [ReplyBy]) VALUES (N'57bef58f-3b49-4c13-95b4-f53f8974cf4d', N'57bef58f-3b49-4c13-95b4-f53f8974cf1d', N'Reply 3', N'2021-10-18 17:17:59', N'jerry1234')
INSERT INTO [dbo].[Replies] ([ReplyGUID], [DiscGUID], [ReplyContent], [ReplyDate], [ReplyBy]) VALUES (N'57bef58f-3b49-4c13-95b4-f53f8974cf5d', N'57bef58f-3b49-4c13-95b4-f53f8974cc1d', N'Test', N'2021-10-19 18:17:59', N'jerry1234')

INSERT INTO [dbo].[ForumReport] ([ForumReportGUID], [DiscGUID], [ReplyGUID], [ReportReason], [ReportStatus],[ReportBy], [ReportDate]) VALUES (N'88eb7546-0195-43c1-aa66-52a30d0fc806', N'8bfccfbf-863b-4e09-83c4-76e4ad2d0e96', N'2492422e-ff10-46c1-ab61-f04b6ebbb37b', N'Advertising', N'In Progress',N'krysty12', N'2021-10-22 16:25:26')
