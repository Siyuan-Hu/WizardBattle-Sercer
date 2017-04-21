module.exports = function(app) {
  return new Handler(app);
};

var Handler = function(app) {
  this.app = app;
  this.channelService=this.app.get('channelService');
};

/**
 * New client entry.
 *
 * @param  {Object}   msg     request message
 * @param  {Object}   session current session object
 * @param  {Function} next    next step callback
 * @return {Void}
 */
Handler.prototype.entry = function(msg, session, next) {
	session.set('rid', 1);
	session.bind(session.id);
	channel = this.channelService.getChannel(1, false);
	console.log(session);
	if (!!channel)
	{
		channel.add(session.id,this.app.get('serverId'));
	}
	else
	{
		this.channelService.createChannel(1);
		channel = this.channelService.getChannel(1, false);
		channel.add(session.id,this.app.get('serverId'));
	}

	next(null, {code: 200, msg: 'game server is ok.'});
};

// player move
Handler.prototype.move = function(msg, session, next) {
  next(null, {code: 200, msg: msg.direction});
};

// send to all client
Handler.prototype.send = function(msg, session, next) {
	var rid = session.get('rid');
	// var username = session.uid.split('*')[0];
	var param = {
		msg: msg.direction,
	};
	channel = this.channelService.getChannel(1, false);

	console.log(channel);

	// //the target is all users
	channel.pushMessage('onChat', param);

	next(null, {
		msg: msg.direction
	});
};

/**
 * Publish route for mqtt connector.
 *
 * @param  {Object}   msg     request message
 * @param  {Object}   session current session object
 * @param  {Function} next    next step callback
 * @return {Void}
 */
Handler.prototype.publish = function(msg, session, next) {
	var result = {
		topic: 'publish',
		payload: JSON.stringify({code: 200, msg: 'publish message is ok.'})
	};
  next(null, result);
};

/**
 * Subscribe route for mqtt connector.
 *
 * @param  {Object}   msg     request message
 * @param  {Object}   session current session object
 * @param  {Function} next    next step callback
 * @return {Void}
 */
Handler.prototype.subscribe = function(msg, session, next) {
	var result = {
		topic: 'subscribe',
		payload: JSON.stringify({code: 200, msg: 'subscribe message is ok.'})
	};
  next(null, result);
};
