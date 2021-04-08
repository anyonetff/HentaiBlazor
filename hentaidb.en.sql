-- 写入导航数据.

DELETE FROM s_function;

INSERT INTO s_function (f_id, f_parent, f_leaf, f_path, f_name, f_icon, f_note, x_insert_, x_update_)
VALUES ('10', '0', true, '/', 'Welcome', 'home', '', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO s_function (f_id, f_parent, f_leaf, f_path, f_name, f_icon, f_note, x_insert_, x_update_)
VALUES ('20', '0', true, '/anime/index', 'Anime', 'play-square', '', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO s_function (f_id, f_parent, f_leaf, f_path, f_name, f_icon, f_note, x_insert_, x_update_)
VALUES ('30', '0', true, '/comic/index', 'Comic', 'read', '', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);


INSERT INTO s_function (f_id, f_parent, f_leaf, f_path, f_name, f_icon, f_note, x_insert_, x_update_)
VALUES ('60', '0', false, '', 'Content', 'appstore', '', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO s_function (f_id, f_parent, f_leaf, f_path, f_name, f_icon, f_note, x_insert_, x_update_)
VALUES ('6010', '60', true, '/basic/catalog/list', 'Directory', 'folder', '', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO s_function (f_id, f_parent, f_leaf, f_path, f_name, f_icon, f_note, x_insert_, x_update_)
VALUES ('6020', '60', true, '/anime/video/list', 'Anime Management', 'video-camera', '', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
INSERT INTO s_function (f_id, f_parent, f_leaf, f_path, f_name, f_icon, f_note, x_insert_, x_update_)
VALUES ('6030', '60', true, '/comic/book/list', 'Comic Management', 'book', '', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO s_function (f_id, f_parent, f_leaf, f_path, f_name, f_icon, f_note, x_insert_, x_update_)
VALUES ('6060', '60', true, '/basic/author/list', 'Authors', 'team', '', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
INSERT INTO s_function (f_id, f_parent, f_leaf, f_path, f_name, f_icon, f_note, x_insert_, x_update_)
VALUES ('6070', '60', true, '/basic/tag/list', 'Tags', 'tags', '', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO s_function (f_id, f_parent, f_leaf, f_path, f_name, f_icon, f_note, x_insert_, x_update_)
VALUES ('80', '0', false, '', 'System', 'setting', '', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
INSERT INTO s_function (f_id, f_parent, f_leaf, f_path, f_name, f_icon, f_note, x_insert_, x_update_)
VALUES ('8010', '80', true, '/security/function/list', 'Menu', 'bars', '', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
INSERT INTO s_function (f_id, f_parent, f_leaf, f_path, f_name, f_icon, f_note, x_insert_, x_update_)
VALUES ('8050', '80', true, '/security/user/list', 'User', 'idcard', '', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

-- 写入标签数据

INSERT INTO b_tag (t_id, t_name, t_alias, t_items, t_note, x_insert_, x_update_)
VALUES ('.futanari', 'futanari', '.', 0, '预设分类', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
INSERT INTO b_tag (t_id, t_name, t_alias, t_items, t_note, x_insert_, x_update_)
VALUES ('.yuri', 'yuri', '.', 0, '预设分类', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
INSERT INTO b_tag (t_id, t_name, t_alias, t_items, t_note, x_insert_, x_update_)
VALUES ('.ahegao', 'ahegao', '.', 0, '预设分类', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
INSERT INTO b_tag (t_id, t_name, t_alias, t_items, t_note, x_insert_, x_update_)
VALUES ('.netorare', 'netorare', '.', 0, '预设分类', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
INSERT INTO b_tag (t_id, t_name, t_alias, t_items, t_note, x_insert_, x_update_)
VALUES ('.elf', 'elf', '.', 0, '预设分类', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
INSERT INTO b_tag (t_id, t_name, t_alias, t_items, t_note, x_insert_, x_update_)
VALUES ('.succubus', 'succubus', '.', 0, '预设分类', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
