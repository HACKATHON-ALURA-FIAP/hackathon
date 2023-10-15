var heatmapData = [
  {location: new google.maps.LatLng(37.782, -122.447), weight: 0.5},
  new google.maps.LatLng(37.782, -122.445),
  {location: new google.maps.LatLng(-23.803080607327054, -46.81136629489188), weight: 2},
  {location: new google.maps.LatLng(-23.803080607327054, -46.81136629489187), weight: 3},
  {location: new google.maps.LatLng(-23.81003517433402, -46.84599155317186), weight: 2},
  new google.maps.LatLng(-23.81003517433402, -46.84599155317185),
  {location: new google.maps.LatLng(-23.82982865045899, -46.85084992877076), weight: 0.5},
  ];
  
  var embuGuacu = new google.maps.LatLng(-23.83287108977146, -46.816376088976426);
  
  map = new google.maps.Map(document.getElementById('map'), {
    center: embuGuacu,
    zoom: 14,
    mapTypeId: 'satellite'
  });
  
  var heatmap = new google.maps.visualization.HeatmapLayer({
    data: heatmapData
  });
  heatmap.setMap(map);

  //linha
