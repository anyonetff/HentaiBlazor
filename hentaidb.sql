DELETE FROM s_function;

INSERT INTO s_function (f_id, f_parent, f_path, f_name, f_icon)
VALUES ('10', '0', '/', '欢迎', 'appstore');

INSERT INTO s_function (f_id, f_parent, f_path, f_name, f_icon)
VALUES ('20', '0', '/anime/video/list', '动画', 'play-square');
INSERT INTO s_function (f_id, f_parent, f_path, f_name, f_icon)
VALUES ('30', '0', '/comic/book/list', '漫画', 'picture');

INSERT INTO s_function (f_id, f_parent, f_path, f_name, f_icon)
VALUES ('50', '0', '/basic/catalog/list', '目录', 'folder');
INSERT INTO s_function (f_id, f_parent, f_path, f_name, f_icon)
VALUES ('60', '0', '/basic/author/list', '作者', 'team');
INSERT INTO s_function (f_id, f_parent, f_path, f_name, f_icon)
VALUES ('65', '0', '/basic/tag/list', '标签', 'tags');
