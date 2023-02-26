<template>
    <aside class="sidebar-shop sidebar-filter sidebar-filter-banner">
        <div class="sidebar-filter-wrapper">
            <div class="widget widget-clean">
                <button @click="closeSidebar">
                    <i class="icon-close"></i>Filters
                </button>
                <a href="#" class="sidebar-filter-clear" @click.prevent="cleanAllFilter">Clean All</a>
            </div>
            <div class="widget">
                <h3 class="widget-title">Browse Category</h3>

                <div class="widget-body">
                    <div class="filter-items filter-items-count">
                        <div class="filter-item">
                            <div class="custom-control custom-checkbox">
                                <input type="checkbox" class="custom-control-input" id="cat-1" />
                                <label class="custom-control-label" for="cat-1">Women</label>
                            </div>

                            <span class="item-count">3</span>
                        </div>

                        <div class="filter-item">
                            <div class="custom-control custom-checkbox">
                                <input type="checkbox" class="custom-control-input" id="cat-2" />
                                <label class="custom-control-label" for="cat-2">Men</label>
                            </div>

                            <span class="item-count">0</span>
                        </div>

                        <div class="filter-item">
                            <div class="custom-control custom-checkbox">
                                <input type="checkbox" class="custom-control-input" id="cat-3" />
                                <label class="custom-control-label" for="cat-3">Holiday Shop</label>
                            </div>

                            <span class="item-count">0</span>
                        </div>

                        <div class="filter-item">
                            <div class="custom-control custom-checkbox">
                                <input type="checkbox" class="custom-control-input" id="cat-4" />
                                <label class="custom-control-label" for="cat-4">Gifts</label>
                            </div>

                            <span class="item-count">0</span>
                        </div>

                        <div class="filter-item">
                            <div class="custom-control custom-checkbox">
                                <input type="checkbox" class="custom-control-input" id="cat-5" />
                                <label class="custom-control-label" for="cat-5">Homeware</label>
                            </div>

                            <span class="item-count">0</span>
                        </div>

                        <div class="filter-item">
                            <div class="custom-control custom-checkbox">
                                <input
                                    type="checkbox"
                                    class="custom-control-input"
                                    id="cat-6"
                                    checked="checked"
                                />
                                <label
                                    class="custom-control-label"
                                    for="cat-6"
                                >Grid Categories Fullwidth</label>
                            </div>

                            <span class="item-count">13</span>
                        </div>

                        <div class="sub-filter-items">
                            <div class="filter-item">
                                <div class="custom-control custom-checkbox">
                                    <input type="checkbox" class="custom-control-input" id="cat-7" />
                                    <label class="custom-control-label" for="cat-7">Dresses</label>
                                </div>

                                <span class="item-count">3</span>
                            </div>

                            <div class="filter-item">
                                <div class="custom-control custom-checkbox">
                                    <input type="checkbox" class="custom-control-input" id="cat-8" />
                                    <label class="custom-control-label" for="cat-8">T-shirts</label>
                                </div>

                                <span class="item-count">0</span>
                            </div>

                            <div class="filter-item">
                                <div class="custom-control custom-checkbox">
                                    <input type="checkbox" class="custom-control-input" id="cat-9" />
                                    <label class="custom-control-label" for="cat-9">Bags</label>
                                </div>

                                <span class="item-count">4</span>
                            </div>

                            <div class="filter-item">
                                <div class="custom-control custom-checkbox">
                                    <input type="checkbox" class="custom-control-input" id="cat-10" />
                                    <label class="custom-control-label" for="cat-10">Jackets</label>
                                </div>

                                <span class="item-count">2</span>
                            </div>

                            <div class="filter-item">
                                <div class="custom-control custom-checkbox">
                                    <input type="checkbox" class="custom-control-input" id="cat-11" />
                                    <label class="custom-control-label" for="cat-11">Shoes</label>
                                </div>

                                <span class="item-count">2</span>
                            </div>

                            <div class="filter-item">
                                <div class="custom-control custom-checkbox">
                                    <input type="checkbox" class="custom-control-input" id="cat-12" />
                                    <label class="custom-control-label" for="cat-12">Jumpers</label>
                                </div>

                                <span class="item-count">1</span>
                            </div>

                            <div class="filter-item">
                                <div class="custom-control custom-checkbox">
                                    <input type="checkbox" class="custom-control-input" id="cat-13" />
                                    <label class="custom-control-label" for="cat-13">Jeans</label>
                                </div>

                                <span class="item-count">1</span>
                            </div>

                            <div class="filter-item">
                                <div class="custom-control custom-checkbox">
                                    <input type="checkbox" class="custom-control-input" id="cat-14" />
                                    <label class="custom-control-label" for="cat-14">Sportwear</label>
                                </div>

                                <span class="item-count">0</span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </aside>
</template>
<script>
export default {
    created: function () {
        document
                .querySelector('body')
                .classList.remove('sidebar-filter-active');
    },
    methods: {
        cleanAllFilter: function() {
            let allInputs = document.querySelectorAll(
                '.sidebar-filter input[type=checkbox]'
            );
            for (let i = 0; i < allInputs.length; i++) {
                allInputs[i].checked = false;
            }
        },
        closeSidebar: function() {
            document
                .querySelector('body')
                .classList.remove('sidebar-filter-active');
        }
    }
};
</script>