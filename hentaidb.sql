-- 写入导航数据.

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

INSERT INTO s_function (f_id, f_parent, f_path, f_name, f_icon, f_note, x_insert_, x_update_)
VALUES ('80', '0', '/security/function/list', '菜单', 'bars', '', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
INSERT INTO s_function (f_id, f_parent, f_path, f_name, f_icon, f_note, x_insert_, x_update_)
VALUES ('85', '0', '/security/user/list', '用户', 'idcard', '', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

-- 写入标签数据

INSERT INTO b_tag (t_id, t_name, t_alias, t_items, t_note, x_insert_, x_update_)
VALUES ('.futanari', '扶她', '.', 0, '预设分类', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
INSERT INTO b_tag (t_id, t_name, t_alias, t_items, t_note, x_insert_, x_update_)
VALUES ('.yuri', '百合', '.', 0, '预设分类', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
INSERT INTO b_tag (t_id, t_name, t_alias, t_items, t_note, x_insert_, x_update_)
VALUES ('.ahegao', '啊嘿颜', '.', 0, '预设分类', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
INSERT INTO b_tag (t_id, t_name, t_alias, t_items, t_note, x_insert_, x_update_)
VALUES ('.netorare', '牛头人', '.', 0, '预设分类', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
INSERT INTO b_tag (t_id, t_name, t_alias, t_items, t_note, x_insert_, x_update_)
VALUES ('.elf', '精灵', '.', 0, '预设分类', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
INSERT INTO b_tag (t_id, t_name, t_alias, t_items, t_note, x_insert_, x_update_)
VALUES ('.succubus', '魅魔', '.', 0, '预设分类', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
