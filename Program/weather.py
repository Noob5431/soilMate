from datetime import datetime
import matplotlib.pyplot as plt
from meteostat import Point, Daily, Normals,Stations,Monthly
import sys

longitude = int(sys.argv[1]) / 1000000
latitude = int(sys.argv[2]) / 1000000

stations = Stations()
stations = stations.nearby(latitude, longitude)
station = stations.fetch(1)

start = datetime(2018,1,1)
end = datetime(2019,1,1)
#data = Normals(station, 1991, 2020)
data = Monthly(station,start,end)
data = data.fetch()
tmin = data.tmin.min()
tmax = data.tmax.max()

print(tmin,";",tmax,";")