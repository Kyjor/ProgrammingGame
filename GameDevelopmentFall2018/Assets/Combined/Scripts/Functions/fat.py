from mininet.topo import Topo
import logging
import os

class FatTree( Topo ):
  
    CoreSwitches = []
    AggSwitches = []
    EdgeSwitches = []
    HostList = []
 
    def __init__( self, k):
        self.pod = k
        self.CoreSwitch = (k/2)**2
        self.AggSwitch = k*k/2
        self.EdgeSwitch = k*k/2
        self.density = k/2
        self.Host = self.EdgeSwitch * self.density
        
        self.c2a = 0.2
        self.a2e = 0.1
        self.h2a = 0.05

        # Init Topo
        Topo.__init__(self)
  
        self.create()

        self.createLink( c2a=self.c2a, 
                         a2e=self.a2e, 
                         h2a=self.h2a)

    
    def create(self):
        self.createCoreSwitch(self.CoreSwitch)
        self.createAggSwitch(self.AggSwitch)
        self.createEdgeSwitch(self.EdgeSwitch)
        self.createHost(self.Host)


    def _addSwitch(self, num, lev, switches):
        for x in xrange(1, num+1):
            pre = str(lev) + "00"
            if x >= int(10):
                pre = str(lev) + "0"
            switches.append(self.addSwitch('s' + pre + str(x)))

    def createCoreSwitch(self, NUM):
        self._addSwitch(NUM, 1, self.CoreSwitches)

    def createAggSwitch(self, NUM):
        self._addSwitch(NUM, 2, self.AggSwitches)

    def createEdgeSwitch(self, NUM):
        self._addSwitch(NUM, 3, self.EdgeSwitches)

    def createHost(self, NUM):
        for x in xrange(1, NUM+1):
            pre = "h00"
            if x >= int(10):
                pre = "h0"
            elif x >= int(100):
                pre = "h"
            self.HostList.append(self.addHost(pre + str(x)))

    def createLink(self, c2a=0.2, a2e=0.1, h2a=0.5):
        end = self.pod/2
        for x in xrange(0, self.AggSwitch, end):
            for i in xrange(0, end):
                for j in xrange(0, end):
                    linkopts = dict(bw=c2a) 
                    self.addLink(
                        self.CoreSwitches[i*end+j],
                        self.AggSwitches[x+i],
                        **linkopts)

        for x in xrange(0, self.AggSwitch, end):
            for i in xrange(0, end):
                for j in xrange(0, end):
                    linkopts = dict(bw=a2e) 
                    self.addLink(
                        self.AggSwitches[x+i], self.EdgeSwitches[x+j],
                        **linkopts)

        for x in xrange(0, self.EdgeSwitch):
            for i in xrange(0, self.density):
                linkopts = dict(bw=h2a) 
                self.addLink(
                    self.EdgeSwitches[x],
                    self.Hosts[self.density * x + i],
                    **linkopts)
        
topos = { 'fattree' : ( lambda k : FatTree(k)) }

def GraphTopo(topo, outfile):
  "Graph a fat tree topology"
  switches = []
  hosts = []
  g = nx.DiGraph()
  for switch in topo.switches():
    switches.append(switch.replace(',', '\n'))
  for host in topo.hosts():
    hosts.append(host.replace(',', '\n'))
  g.add_nodes_from(switches)
  g.add_nodes_from(hosts)
  g.add_nodes_from(topo.switches() + topo.hosts())
  g.add_edges_from(topo.links())
  for link in topo.links():
    g.add_edge(link[0].replace(',', '\n'), link[1].replace(',', '\n'))
  # Create the graphviz layout
  size = len(topo.hosts())
  plt.figure(figsize=(size, size / 2))
  pos = nx.graphviz_layout(g, prog='dot')
  # Draw the switches, hosts, edges, and labels
  nx.draw_networkx_nodes(g, pos, nodelist=switches, node_size=1500, node_color='gray')
  nx.draw_networkx_nodes(g, pos, nodelist=hosts, node_size=1200, node_color='green')
  nx.draw_networkx_edges(g, pos, arrows=False)
  nx.draw_networkx_labels(g, pos)
  # Save the graph to output file
  plt.axis('off')
  plt.margins(0)
  plt.title('Fat Tree Topology (k=' + str(topo.k) + ')')
  plt.savefig(outfile, bbox_inches='tight', pad_inches=0.1)
  return
  
  