<template>
    <div
        class="sidebar-shop pr-4"
        :class="isSidebar ? 'sidebar-filter' : 'sidebar'"
    >
        <div :class="{ 'sidebar-filter-wrapper': isSidebar }">
            <div class="widget widget-clean pl-1 pr-2">
                <label>Bộ lọc:</label>
                <a
                    href="#"
                    class="sidebar-filter-clear"
                    @click.prevent="cleanAll"
                    >Xóa tất cả</a
                >
            </div>

            <div class="widget pl-1 pr-2">
                <h3 class="widget-title mb-2">
                    <a href="#widget-5">Giá tiền</a>
                </h3>

                <vue-slide-toggle class="show" :open="true">
                    <div class="widget-body pt-0">
                        <div class="filter-price">
                            <vue-nouislider
                                :config="priceSliderConfig"
                                :values="priceValues"
                                class="mb-2"
                            ></vue-nouislider>
                            <p>
                                {{ formatPrice(priceValues[0], false) }}
                                - {{ formatPrice(priceValues[1], false) }}
                            </p>
                            <br />
                            <a
                                href="#"
                                class="sidebar-filter-clear"
                                @click.prevent="applyPriceRange"
                                >Áp dụng</a
                            >
                        </div>
                    </div>
                </vue-slide-toggle>
            </div>

            <div class="widget pl-1 pr-2">
                <h3 class="widget-title mb-2">
                    <a href="#widget-2">Phân loại</a>
                </h3>

                <vue-slide-toggle :open="true" class="show pb-2">
                    <div class="widget-body pt-0">
                        <div class="filter-items">
                            <p v-if="!categories.length">
                                Chưa có phân loại được tạo.
                            </p>
                            <div
                                v-else
                                class="filter-item"
                                v-for="(item, index) in categories"
                                :key="index"
                            >
                                <div class="custom-control custom-checkbox">
                                    <input
                                        type="checkbox"
                                        class="custom-control-input"
                                        :id="'size-' + index"
                                        @click="onCategoryClick(item)"
                                        v-model="selectedCategories"
                                        :value="item.slug"
                                    />
                                    <label
                                        class="custom-control-label"
                                        :for="'size-' + index"
                                        >{{ item.name }} ({{
                                            item.count
                                        }})</label
                                    >
                                </div>
                            </div>
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
import { mapGetters, mapMutations, mapActions } from 'vuex';

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
            priceValues: [0, 150000000],
            priceSliderConfig: {
                connect: true,
                step: 100000,
                margin: 100000,
                range: {
                    min: 0,
                    max: 150000000
                }
            },
            filterData: shopData,
            selectedCategories: []
        };
    },
    computed: {
        ...mapGetters('category', ['categories']),
        priceRangeText: function() {
            return (
                '$' +
                parseInt(this.priceValues[0]) +
                ' — $' +
                parseInt(this.priceValues[1])
            );
        },
        priceFilterRoute: function() {
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

            return {
                path: this.$route.path,
                query: {
                    ...query,
                    minPrice: this.priceValues[0],
                    maxPrice: this.priceValues[1]
                }
            };
        }
    },
    created: function() {
        this.renderCategories(this.$companyName);
        document
            .querySelector('body')
            .classList.remove('sidebar-filter-active');
        if (this.$route.query.minPrice && this.$route.query.maxPrice) {
            this.loaded = false;
            this.priceValues = [
                this.$route.query.minPrice,
                this.$route.query.maxPrice
            ];
            this.$nextTick(function() {
                this.loaded = true;
            });
        } else {
            this.loaded = false;
            this.$nextTick(function() {
                this.loaded = true;
            });
        }
    },
    methods: {
        ...mapActions('category', ['renderCategories']),
        ...mapActions('product', ['search']),
        ...mapMutations('product', [
            'SET_PRODUCT_CATEGORY',
            'SET_PRODUCT_PRICE_RANGE',
            'CLEAN_QUERY_OPTIONS'
        ]),
        async onCategoryClick(item) {
            this.SET_PRODUCT_CATEGORY(item.slug);
            await this.search({
                company: this.$companyName,
                reload: true
            });
        },
        async applyPriceRange() {
            this.SET_PRODUCT_PRICE_RANGE(this.priceValues);
            await this.search({
                company: this.$companyName,
                reload: true
            });
        },
        async cleanAll() {
            this.CLEAN_QUERY_OPTIONS();
            this.priceValues = [0, 200000000];
            this.selectedCategories = [];
            this.$forceUpdate();
            await this.search({
                company: this.$companyName,
                reload: true
            });
        },
        categorySelected: function(item) {},
        setSizeFilter: function(item) {}
    }
};
</script>

<style lang="scss">
#filter-price-range {
    display: none;
}
</style>
