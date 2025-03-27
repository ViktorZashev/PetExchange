//Филтри
function onFilterClick(option) {
	if (option.classList.contains("selected")) {
		unSelectFilter(option);
	}
	else {
		selectFilter(option);
	}
}

function selectFilter(option) {
	var value = null;
	var queryname = null;
	if (option.dataset.query) {
		queryname = option.dataset.query;
	}
	if (option.dataset.value) {
		value = option.dataset.value;
	}
	if (!queryname || !value) return;
	applyChangeToUrl(queryname, value, false);
}

function unSelectFilter(option) {
	var value = null;
	var queryname = null;
	if (option.dataset.query) {
		queryname = option.dataset.query;
	}
	if (option.dataset.value) {
		value = option.dataset.value;
	}
	if (!queryname || !value) return;
	applyChangeToUrl(queryname, value, true);
}

function applyChangeToUrl(queryName, value, isRemove) {
	var params = getCurrentQuery();
	if (isRemove) {
		if (params.hasOwnProperty(queryName)) {
			var values = Array.from(params[queryName]);
			var valueIndex = values.indexOf(value);
			if (valueIndex > -1) {
				values.splice(valueIndex, 1);
			}
			params[queryName] = values;
			if (values.length == 0) {
				delete params[queryName];
			}
		}
	}
	else {
		if (!params.hasOwnProperty(queryName)) {
			params[queryName] = [];
		}
		var values = Array.from(params[queryName]);
		var valueIndex = values.indexOf(value);
		if (valueIndex == -1) {
			values.push(value);
		}
		params[queryName] = values;
	}
	if (params.page)
		delete params.page;
	var paramList = [];
	for (var propName in params) {
		paramList.push(propName + "=" + params[propName].join(","))
	}
	window.location.href = window.location.pathname + "?" + paramList.join("&");

}

function getCurrentQuery() {
	var urlParams = new URLSearchParams(window.location.search);
	const params = {};
	for (const [key, value] of urlParams.entries()) {
		params[key] = value.split(",");
	}
	return params;
}

function clearFiltersClick() {
	window.location.href = window.location.pathname;
}


document.addEventListener("DOMContentLoaded", function () {

	var clearFiltersEl = document.getElementById("clear-filters");
	if (clearFiltersEl) {
		clearFiltersEl.addEventListener("click", function (ev) {
			ev.stopPropagation();
			ev.preventDefault();
			clearFiltersClick();
		});
	}
	var allFilterOptions = document.querySelectorAll(".filter-option");
	allFilterOptions.forEach(function (option) {
		option.addEventListener("click", function (ev) {
			ev.stopPropagation();
			ev.preventDefault();
			var filterOption = ev.target.closest(".filter-option");
			onFilterClick(filterOption);
		});
	});
})