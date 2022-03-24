using static System.Console;

class IntList {
    int[] a = new int[4];
    int count;
    public void append(int i) {
        if (count == a.Length) {
            int[] b = new int[a.Length * 2];
            for (int j = 0; j < a.Length; ++j)
                b[j] = a[j];
            a = b;
        }
        a[count++] = i;
    }
    public int length => count;
    public int this[int i] => a[i];
}


class bp_graph{ 
    static void printGraph(IntList[] g, int n){
        for (int i=1; i<=n; i++){
            WriteLine($"vertex {i}");
            for (int j=0; j<=g[i].length; j++){
                WriteLine($"    neighs {g[i][j]}");
            }
        }
    }

    public static (bool[] g1, bool[] g2, bool is_possible) dfs(IntList[] g, int cities){
        bool[] g1 = new bool[cities+1];
        bool[] g2 = new bool[cities+1];
        bool[] visited = new bool[cities+1];
        bool is_possible = true;
        int visited_cities = 1;

        void visit(int v){
            //WriteLine($"visiting {v}");

            // end if we have visited all cities
            if (visited_cities == cities || !is_possible) return;
            visited[v] = true;

            // explore v's neighbors
            for (int c=0; c<g[v].length; c++){
                int w = g[v][c];
                // if w is not in visited then add it to the group that v is not in
                if (!visited[w] && w!=0){
                    visited_cities++;
                    if (g1[v] && !g1[w]){
                        g2[w]= true;
                    } else if (g2[v] && !g2[w]){
                        g1[w]= true;
                    }
                    visit(w);
                } else if (visited[w] && w!=0) { 
                    // check and make sure V and W are in opposite groups
                    if ((g1[v] && g2[w]) || (g1[w] && g2[v])){
                        continue;
                    } else {
                        is_possible = false;
                        return;
                    }
                }
            }
        }
        // add city 1 to group 1
        g1[1] = true;
        for (int i=1; i<=cities; i++){
            if (!visited[i]){
                visit(i);
            }
        }
        return (g1, g2, is_possible);
    }

    static void Main(){
        // input and setup
        int cities = int.Parse(ReadLine()!);
        int roads = int.Parse(ReadLine()!);

        IntList[] g = new IntList[cities+1];
        for (int k=0; k<=cities; k++){
            IntList a = new IntList();
            g[k] = a;
        }

        while(ReadLine() is string s){
            string[] vals = s.Split();
            int a = int.Parse(vals[0]);
            int b = int.Parse(vals[1]);
            g[a].append(b);
            g[b].append(a);
        }

        // base case
        if (cities == 2 && roads == 0){
            WriteLine("1");
            WriteLine("2");
            return;
        } else if (cities > 2 && roads == 0) {
            WriteLine("Nelze");
            return;
        }
        
        //printGraph(g, cities);

        // dfs
        (bool[] g1, bool[] g2, bool is_possible) = dfs(g, cities);

        // output
        if (is_possible){
            for (int o=0; o<g1.Length; o++) {
                if (g1[o]) 
                    Write($"{o} ");
            } 
            WriteLine();
            for (int p=0; p<g1.Length; p++) {
                if (g2[p]) 
                    Write($"{p} ");
            }
            WriteLine();
        } else {
            WriteLine("Nelze");
        }
        return;
    }
}