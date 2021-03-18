DELETE FROM s_function;

INSERT INTO s_function (f_id, f_parent, f_path, f_name, f_icon, f_note, x_insert_, x_update_)
VALUES ('10', '0', '/', '欢迎', 'appstore', '', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO s_function (f_id, f_parent, f_path, f_name, f_icon, f_note, x_insert_, x_update_)
VALUES ('20', '0', '/anime/video/list', '动画', 'play-square', '', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
INSERT INTO s_function (f_id, f_parent, f_path, f_name, f_icon, f_note, x_insert_, x_update_)
VALUES ('30', '0', '/comic/book/list', '漫画', 'picture', '', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO s_function (f_id, f_parent, f_path, f_name, f_icon, f_note, x_insert_, x_update_)
VALUES ('50', '0', '/basic/catalog/list', '目录', 'folder', '', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
INSERT INTO s_function (f_id, f_parent, f_path, f_name, f_icon, f_note, x_insert_, x_update_)
VALUES ('60', '0', '/basic/author/list', '作者', 'team', '', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
INSERT INTO s_function (f_id, f_parent, f_path, f_name, f_icon, f_note, x_insert_, x_update_)
VALUES ('65', '0', '/basic/tag/list', '标签', 'tags', '', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
