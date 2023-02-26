<template>
    <div class="sidebar-shop" :class="isSidebar ? 'sidebar-filter' : 'sidebar'">
        <div :class="{'sidebar-filter-wrapper' : isSidebar}">
            <div class="widget widget-clean">
                <label>Filters:</label>
                <nuxt-link :to="$route.path" class="sidebar-filter-clear">Clean All</nuxt-link>
            </div>
            <div class="widget widget-collapsible">
                <h3 class="widget-title">
                    <a
                        href="#widget-1"
                        :class="{collapsed: !toggleStates[0]}"
                        @click.prevent="toggleSlide(0)"
                    >Category</a>
                </h3>

                <vue-slide-toggle :open="toggleStates[0]" class="show" :duration="200">
                    <div class="widget-body">
                        <div class="filter-items filter-items-count">
                            <div
                                class="filter-item"
                                v-for="(category, index) in filterData.categories"
                                :key="index"
                            >
                                <nuxt-link
                                    :to="{path: $route.path, query: {category: category.slug}}"
                                    :class="{active: categorySelected(category)}"
                                >{{ category.name }}</nuxt-link>
                                <span class="item-count">{{ category.count }}</span>
                            </div>
                        </div>
                    </div>
                </vue-slide-toggle>
            </div>

            <div class="widget widget-collapsible">
                <h3 class="widget-title">
                    <a
                        href="#widget-4"
                        :class="{collapsed: !toggleStates[3]}"
                        @click.prevent="toggleSlide(3)"
                    >Brand</a>
                </h3>

                <vue-slide-toggle :open="toggleStates[3]" class="show" :duration="200">
                    <div class="widget-body">
                        <div class="filter-items">
                            <div
                                class="filter-item"
                                v-for="(item, index) in filterData.brands"
                                :key="index"
                            >
                                <div class="custom-control custom-checkbox">
                                    <input
                                        type="checkbox"
                                        class="custom-control-input"
                                        :id="'brand-' + index"
                                        @click="setBrandFilter(item)"
                                        :checked="brandChecked(item)"
                                    />
                                    <label
                                        class="custom-control-label"
                                        :for="'brand-' + index"
                                    >{{ item.brand }}</label>
                                </div>
                            </div>
                        </div>
                    </div>
                </vue-slide-toggle>
            </div>

            <div class="widget widget-collapsible">
                <h3 class="widget-title">
                    <a
                        href="#widget-5"
                        :class="{collapsed: !toggleStates[4]}"
                        @click.prevent="toggleSlide(4)"
                    >Price</a>
                </h3>

                <vue-slide-toggle :open="toggleStates[4]" class="show" :duration="200">
                    <div class="widget-body">
                        <div class="filter-items">
                            <div
                                class="filter-item"
                                v-for="(price, index) in filterData.prices"
                                :key="index"
                            >
                                <div class="custom-control custom-radio">
                                    <input
                                        type="radio"
                                        class="custom-control-input"
                                        :id="'price-' + index"
                                        name="filter-price"
                                        :checked="priceChecked(price.min, price.max)"
                                        @change="setPriceFilter(price.min, price.max)"
                                    />
                                    <label
                                        class="custom-control-label"
                                        :for="'price-' + index"
                                    >{{ price.name }}</label>
                                </div>
                            </div>
                        </div>
                    </div>
                </vue-slide-toggle>
            </div>

            <div class="widget widget-collapsible">
                <h3 class="widget-title">
                    <a
                        href="#widget-2"
                        :class="{collapsed: !toggleStates[1]}"
                        @click.prevent="toggleSlide(1)"
                    >Customer Rating</a>
                </h3>

                <vue-slide-toggle :open="toggleStates[1]" class="show" :duration="200">
                    <div class="widget-body">
                        <div class="filter-items">
                            <div class="filter-item">
                                <div class="custom-control custom-checkbox">
                                    <input
                                        type="checkbox"
                                        class="custom-control-input"
                                        id="cus-rating-1"
                                        :checked="ratingChecked('5')"
                                        @change="setRatingFilter('5')"
                                    />
                                    <label class="custom-control-label" for="cus-rating-1">
                                        <span class="ratings-container">
                                            <span class="ratings">
                                                <span class="ratings-val" style="width: 100%;"></span>
                                            </span>

                                            <span class="ratings-text"></span>
                                        </span>
                                    </label>
                                </div>
                            </div>

                            <div class="filter-item">
                                <div class="custom-control custom-checkbox">
                                    <input
                                        type="checkbox"
                                        class="custom-control-input"
                                        id="cus-rating-2"
                                        :checked="ratingChecked('4')"
                                        @change="setRatingFilter('4')"
                                    />
                                    <label class="custom-control-label" for="cus-rating-2">
                                        <span class="ratings-container">
                                            <span class="ratings">
                                                <span class="ratings-val" style="width: 80%;"></span>
                                            </span>

                                            <span class="ratings-text"></span>
                                        </span>
                                    </label>
                                </div>
                            </div>

                            <div class="filter-item">
                                <div class="custom-control custom-checkbox">
                                    <input
                                        type="checkbox"
                                        class="custom-control-input"
                                        id="cus-rating-3"
                                        :checked="ratingChecked('3')"
                                        @change="setRatingFilter('3')"
                                    />
                                    <label class="custom-control-label" for="cus-rating-3">
                                        <span class="ratings-container">
                                            <span class="ratings">
                                                <span class="ratings-val" style="width: 60%;"></span>
                                            </span>

                                            <span class="ratings-text"></span>
                                        </span>
                                    </label>
                                </div>
                            </div>

                            <div class="filter-item">
                                <div class="custom-control custom-checkbox">
                                    <input
                                        type="checkbox"
                                        class="custom-control-input"
                                        id="cus-rating-4"
                                        :checked="ratingChecked('2')"
                                        @change="setRatingFilter('2')"
                                    />
                                    <label class="custom-control-label" for="cus-rating-4">
                                        <span class="ratings-container">
                                            <span class="ratings">
                                                <span class="ratings-val" style="width: 40%;"></span>
                                            </span>

                                            <span class="ratings-text"></span>
                                        </span>
                                    </label>
                                </div>
                            </div>

                            <div class="filter-item">
                                <div class="custom-control custom-checkbox">
                                    <input
                                        type="checkbox"
                                        class="custom-control-input"
                                        id="cus-rating-5"
                                        :checked="ratingChecked('1')"
                                        @change="setRatingFilter('1')"
                                    />
                                    <label class="custom-control-label" for="cus-rating-5">
                                        <span class="ratings-container">
                                            <span class="ratings">
                                                <span class="ratings-val" style="width: 20%;"></span>
                                            </span>

                                            <span class="ratings-text"></span>
                                        </span>
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                </vue-slide-toggle>
            </div>

            <div class="widget widget-collapsible">
                <h3 class="widget-title">
                    <a
                        href="#widget-3"
                        :class="{collapsed: !toggleStates[2]}"
                        @click.prevent="toggleSlide(2)"
                    >Colour</a>
                </h3>

                <vue-slide-toggle :open="toggleStates[2]" class="show" :duration="200">
                    <div class="widget-body">
                        <div class="filter-colors">
                            <nuxt-link
                                :to="getColorUrl(item)"
                                :style="{'background-color': item.color}"
                                v-for="(item, index) in filterData.colors"
                                :key="index"
                                :class="{selected: colorSelected(item)}"
                            >
                                <span class="sr-only">{{ item.color_name }}</span>
                            </nuxt-link>
                        </div>
                    </div>
                </vue-slide-toggle>
            </div>
        </div>
    </div>
</template>

<script>
import { VueSlideToggle } from 'vue-slide-toggle';
import { shopData } from '~/utilities/data';

export default {
    components: {
        VueSlideToggle
    },
    props: {
        isSidebar: Boolean
    },
    data: function() {
        return {
            loaded: true,
            toggleStates: [true, true, true, true, true],
            filterData: shopData
        };
    },
    created: function () {
        document
                .querySelector('body')
                .classList.remove('sidebar-filter-active');
    },
    methods: {
        toggleSlide: function(index) {
            this.toggleStates = this.toggleStates.reduce((acc, cur, id) => {
                if (id == index) return [...acc, !cur];
                else return [...acc, cur];
            }, []);
        },
        ratingChecked: function(item) {
            return (
                this.$route.query.rating &&
                this.$route.query.rating.split(',').includes(item)
            );
        },
        brandChecked: function(item) {
            return (
                this.$route.query.brand &&
                this.$route.query.brand.split(',').includes(item.slug)
            );
        },
        priceChecked: function(min, max) {
            let flag = false;
            if (this.$route.query.minPrice && this.$route.query.minPrice == min)
                flag = true;
            else return false;

            if (max) {
                if (
                    this.$route.query.maxPrice &&
                    this.$route.query.maxPrice == max
                )
                    flag = true;
                else return false;
            }
            return true;
        },
        categorySelected: function(item) {
            return (
                this.$route.query.category &&
                this.$route.query.category == item.slug
            );
        },
        colorSelected: function(item) {
            return (
                this.$route.query.color &&
                this.$route.query.color.split(',').includes(item.color_name)
            );
        },
        setRatingFilter: function(item) {
            let query = {};
            if (this.$route.query.page) {
                for (let key in this.$route.query) {
                    if (key !== 'page') {
                        query[key] = this.$route.query[key];
                    }
                }
            } else {
                query = { ...this.$route.query };
            }

            if (!this.$route.query.rating) {
                query = {
                    ...query,
                    rating: item
                };
            } else if (this.$route.query.rating.split(',').includes(item)) {
                query = {
                    ...query,
                    rating: query.rating
                        .split(',')
                        .filter(slug => slug !== item)
                        .join(',')
                };
            } else {
                query = {
                    ...query,
                    rating: [...query.rating.split(','), item].join(',')
                };
            }

            this.$router.push({
                path: this.$route.path,
                query: query
            });
        },
        setBrandFilter: function(item) {
            let query = {};
            if (this.$route.query.page) {
                for (let key in this.$route.query) {
                    if (key !== 'page') {
                        query[key] = this.$route.query[key];
                    }
                }
            } else {
                query = { ...this.$route.query };
            }

            if (!this.$route.query.brand) {
                query = {
                    ...query,
                    brand: item.slug
                };
            } else if (this.$route.query.brand.split(',').includes(item.slug)) {
                query = {
                    ...query,
                    brand: query.brand
                        .split(',')
                        .filter(slug => slug !== item.slug)
                        .join(',')
                };
            } else {
                query = {
                    ...query,
                    brand: [...query.brand.split(','), item.slug].join(',')
                };
            }

            this.$router.push({
                path: this.$route.path,
                query: query
            });
        },
        getColorUrl: function(item) {
            let query = {};

            if (!this.$route.query.color) {
                query = {
                    ...this.$route.query,
                    color: item.color_name
                };
            } else if (
                this.$route.query.color.split(',').includes(item.color_name)
            ) {
                query = {
                    ...this.$route.query,
                    color: this.$route.query.color
                        .split(',')
                        .filter(slug => slug !== item.color_name)
                        .join(',')
                };
            } else {
                query = {
                    ...this.$route.query,
                    color: [
                        ...this.$route.query.color.split(','),
                        item.color_name
                    ].join(',')
                };
            }

            let temp = {};
            if (query.page) {
                for (let key in query) {
                    if (key !== 'page') {
                        temp[key] = query[key];
                    }
                }
            } else {
                temp = { ...query };
            }

            return {
                path: this.$route.path,
                query: temp
            };
        },
        setPriceFilter: function(min, max) {
            let query = {};
            if (this.$route.query.page) {
                for (let key in this.$route.query) {
                    if (key !== 'page') {
                        query[key] = this.$route.query[key];
                    }
                }
            } else {
                query = { ...this.$route.query };
            }

            query = {
                ...query,
                minPrice: min
            };

            if (max) {
                query = {
                    ...query,
                    maxPrice: max
                };
            } else {
                let temp = {};
                for (let key in query) {
                    if (key !== 'maxPrice') {
                        temp[key] = query[key];
                    }
                }
                query = { ...temp };
            }

            this.$router.push({
                path: this.$route.path,
                query: query
            });
        }
    }
};
</script>