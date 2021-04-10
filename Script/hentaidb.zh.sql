-- 写入导航数据.

DELETE FROM s_function;

INSERT INTO s_function (f_id, f_parent, f_leaf, f_path, f_name, f_icon, f_note, x_insert_, x_update_)
VALUES ('10', '0', true, '/', '欢迎首页', 'home', '', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO s_function (f_id, f_parent, f_leaf, f_path, f_name, f_icon, f_note, x_insert_, x_update_)
VALUES ('20', '0', true, '/anime/index', '里番动画', 'play-square', '', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO s_function (f_id, f_parent, f_leaf, f_path, f_name, f_icon, f_note, x_insert_, x_update_)
VALUES ('30', '0', true, '/comic/index', '热辣漫画', 'read', '', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);


INSERT INTO s_function (f_id, f_parent, f_leaf, f_path, f_name, f_icon, f_note, x_insert_, x_update_)
VALUES ('60', '0', false, '', '内容维护', 'appstore', '', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO s_function (f_id, f_parent, f_leaf, f_path, f_name, f_icon, f_note, x_insert_, x_update_)
VALUES ('6010', '60', true, '/basic/catalog/list', '档案目录', 'folder', '', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO s_function (f_id, f_parent, f_leaf, f_path, f_name, f_icon, f_note, x_insert_, x_update_)
VALUES ('6020', '60', true, '/anime/video/list', '动画管理', 'video-camera', '', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
INSERT INTO s_function (f_id, f_parent, f_leaf, f_path, f_name, f_icon, f_note, x_insert_, x_update_)
VALUES ('6030', '60', true, '/comic/book/list', '漫画管理', 'book', '', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO s_function (f_id, f_parent, f_leaf, f_path, f_name, f_icon, f_note, x_insert_, x_update_)
VALUES ('6050', '60', true, '/basic/producer/list', '制作公司', 'deployment-unit', '', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
INSERT INTO s_function (f_id, f_parent, f_leaf, f_path, f_name, f_icon, f_note, x_insert_, x_update_)
VALUES ('6060', '60', true, '/basic/author/list', '作品作者', 'team', '', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO s_function (f_id, f_parent, f_leaf, f_path, f_name, f_icon, f_note, x_insert_, x_update_)
VALUES ('6070', '60', true, '/basic/tag/list', '标签分类', 'tags', '', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO s_function (f_id, f_parent, f_leaf, f_path, f_name, f_icon, f_note, x_insert_, x_update_)
VALUES ('80', '0', false, '', '系统设置', 'setting', '', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
INSERT INTO s_function (f_id, f_parent, f_leaf, f_path, f_name, f_icon, f_note, x_insert_, x_update_)
VALUES ('8010', '80', true, '/security/function/list', '功能菜单', 'bars', '', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
INSERT INTO s_function (f_id, f_parent, f_leaf, f_path, f_name, f_icon, f_note, x_insert_, x_update_)
VALUES ('8050', '80', true, '/security/user/list', '用户管理', 'idcard', '', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

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
