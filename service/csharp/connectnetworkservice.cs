﻿using System;
using System.Net;
using System.Net.Sockets;

namespace service
{
	public class connectnetworkservice : service
	{
		public connectnetworkservice(juggle.process _process)
		{
			process_ = _process;
		}

		public channel connect(String ip, short port)
		{
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            s.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 0));
            s.Connect(new IPEndPoint(IPAddress.Parse(ip), port));

            channel ch = new channel(s);
            ch.onDisconnect += this.onChannelDisconn;

			process_.reg_channel(ch);

            return ch;
        }

		public delegate void ChannelDisconnectHandle(channel ch);
		public event ChannelDisconnectHandle onChannelDisconnect;

		public void onChannelDisconn(channel ch)
		{
			if (onChannelDisconnect != null)
			{
				onChannelDisconnect(ch);
			}
		}

		public void poll(Int64 tick)
		{
		}

		private juggle.process process_;
	}
}

